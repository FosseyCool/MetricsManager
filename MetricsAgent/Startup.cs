using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Core.Intefaces;
using FluentMigrator.Runner;
using MetricsAgent.DAL;
using MetricsAgent.DAL.Helpers;
using MetricsAgent.DAL.Interfaces;
using MetricsAgent.Jobs;
using MetricsAgent.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Quartz;
using Quartz.Impl;
using Quartz.Spi;

namespace MetricsAgent
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
        
        private const string ConnectionString = @"Data Source=metrics.db; Version=3;";


        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddScoped<ICpuMetricsRepository, CpuMetricsRepository>();
            services.AddScoped<IRamMetricRepository, RamMetricsRepository>();
            services.AddScoped<INetworkMetricsRepository, NetworkMetricsRepository>();
            services.AddScoped<IHddMetricsRepository, HddMetricsRepository>();
            services.AddScoped<IDotNetRepository, DotNetRepository>();
            
            
           // services.AddTransient<INotifier,Notifier1>();
 
            var mapperConfiguration = new MapperConfiguration(mp => mp.AddProfile(new MapperProfile()));
            var mapper = mapperConfiguration.CreateMapper();
            services.AddSingleton(mapper);
            
            services.AddFluentMigratorCore()
                .ConfigureRunner(rb => rb
                    // добавляем поддержку SQLite 
                    .AddSQLite()
                    // устанавливаем строку подключения
                    .WithGlobalConnectionString(ConnectionString)
                    // подсказываем где искать классы с миграциями
                    .ScanIn(typeof(Startup).Assembly).For.Migrations()
                ).AddLogging(lb => lb
                    .AddFluentMigratorConsole());
            
            // ДОбавляем сервисы
            services.AddSingleton<IJobFactory, SingletonJobFactory>();
            services.AddSingleton<ISchedulerFactory, StdSchedulerFactory>();
 // добавляем нашу задачу
            services.AddSingleton<CpuMetricJob>();
            services.AddSingleton<RamMetricJob>();
            services.AddSingleton<DotNetMetricJob>();
            services.AddSingleton<HddMetricJob>();
            services.AddSingleton<NetworkJob>();
            services.AddSingleton(new JobSchedule(
                 typeof(CpuMetricJob),
                 "0/5 * * * * ?")); // запускать каждые 5 секунд
            services.AddSingleton(new JobSchedule(
                 typeof(RamMetricJob),
                 "0/5 * * * * ?"));
            services.AddSingleton(new JobSchedule(
                typeof(DotNetMetricJob),
                "0/5 * * * * ?"));
            services.AddSingleton(new JobSchedule(
                typeof(HddMetricJob),
                "0/5 * * * * ?"));
            services.AddSingleton(new JobSchedule(
                typeof(NetworkJob),
                "0/5 * * * * ?"));

             services.AddHostedService<QuartzHostedService>();

            //services.AddControllers();
            services.AddSingleton<ICpuMetricsRepository, CpuMetricsRepository>();
            services.AddSingleton<IRamMetricRepository, RamMetricsRepository>();
            services.AddSingleton<IDotNetRepository, DotNetRepository>();
            services.AddSingleton<IHddMetricsRepository, HddMetricsRepository>();
            services.AddSingleton<INetworkMetricsRepository, NetworkMetricsRepository>();
         
          

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo {Title = "MetricsAgent", Version = "v1"});
            });
        }
    
        
        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env,IMigrationRunner migrationRunner)
        {

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();

                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "MetricsAgent v1"));
            }

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
            
            // запускаем миграции
            migrationRunner.MigrateUp();

        }
    }
}