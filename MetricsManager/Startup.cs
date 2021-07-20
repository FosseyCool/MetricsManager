using System;
using AutoMapper;
using AutoMapper.Configuration;
using FluentMigrator.Runner;
using MetricsAgent;
using MetricsAgent.DAL;
using MetricsAgent.DAL.Helpers;
using MetricsAgent.Jobs;
using MetricsManager.Client;
using MetricsManager.DAL.Interfaces;
using MetricsManager.DAL.Repositories;
using MetricsManager.Jobs;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Quartz;
using Quartz.Impl;
using Quartz.Spi;
using CpuMetricJob = MetricsManager.Jobs.CpuMetricJob;
using JobSchedule = MetricsManager.Jobs.JobSchedule;
using RamMetricJob = MetricsManager.Jobs.RamMetricJob;

namespace MetricsManager
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<IAgentRepository, AgentsRepository>();
            services.AddSingleton<ICpuMetricRepository, CpuMetricRepository>();
            services.AddSingleton<IDotNetMetricRepository, DotNetMetricRepository>();
            services.AddSingleton<IHardDriveMetricRepository, HardDriveMetricRepository>();
            services.AddSingleton<INetworkRepository, NetworkMetricRepository>();
            services.AddSingleton<IRamMetricRepository, RamMetricRepository>();

            services.AddSingleton<IJobFactory, SingletonJobFactory>();
            services.AddSingleton<ISchedulerFactory, StdSchedulerFactory>();
            
            services.AddSingleton<CpuMetricJob>();
            services.AddSingleton<DotNetMetricJob>();
            services.AddSingleton<HardDriveMetricJob>();
            services.AddSingleton<NetworkMetricJob>();
            services.AddSingleton<RamMetricJob>();
            services.AddSingleton(new JobSchedule(
                typeof(CpuMetricJob), 
                "0/5 * * * * ?"));
            services.AddSingleton(new JobSchedule(
                typeof(DotNetMetricJob), 
                "0/5 * * * * ?"));
            services.AddSingleton(new JobSchedule(
                typeof(HardDriveMetricJob), 
                "0/5 * * * * ?"));
            services.AddSingleton(new JobSchedule(
                typeof(NetworkMetricJob), 
                "0/5 * * * * ?"));
            services.AddSingleton(new JobSchedule(
                typeof(RamMetricJob), 
                "0/5 * * * * ?"));
            
            services.AddHostedService<QuartzHostedService>();
            
            MapperConfiguration mapperConfiguration = new MapperConfiguration(
                mp => mp.AddProfile(new MapperProfile()));
            var mapper = mapperConfiguration.CreateMapper();
            
            services.AddSingleton(mapper);
            
            services.AddFluentMigratorCore()
                .ConfigureRunner(rb => rb
                    .AddSQLite()
                    .WithGlobalConnectionString(ConnectionManager.ConnectionString)
                    .ScanIn(typeof(Startup).Assembly).For.Migrations()
                ).AddLogging(lb => lb
                    .AddFluentMigratorConsole());

            services.AddHttpClient<IMetricsAgentClient, MetricsAgentClient>();
           //     .AddTransientHttpErrorPolicy(p 
             //       => p.WaitAndRetryAsync(3, _ => TimeSpan.FromMilliseconds(1000)));
            
            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo {Title = "MetricsManager", Version = "v1"});
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IMigrationRunner migrationRunner)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "MetricsManager v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
            
            migrationRunner.MigrateUp();
        }
    }
}