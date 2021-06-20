using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Threading.Tasks;
using MetricsAgent.DAL;
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

namespace MetricsAgent
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
            services.AddControllers();
            ConfigureSqlLiteConnection(services);
          

            services.AddScoped<IRamMetricRepository, RamMetricsRepository>();
            
            services.AddScoped<INetworkMetricsRepository, NetworkMetricsRepository>();

            services.AddScoped<IHddMetricsRepository, HddMetricsRepository>();

            services.AddScoped<IDotNetRepository, DotNetRepository>();
            services.AddScoped<ICpuMetricsRepository, CpuMetricsRepository>();
        }

        private void ConfigureSqlLiteConnection(IServiceCollection services)
        {
         const string connectionString = "Data Source=metrics.db;Version=3;Pooling=true;Max Pool Size=100;";
            var connection = new SQLiteConnection(connectionString);
            connection.Open();
            PrepareSchema(connection);
        }

        private void PrepareSchema(SQLiteConnection connection)
        {
            using (var command = new SQLiteCommand(connection))
            {
                // задаем новый текст команды для выполнения
                // удаляем таблицу с метриками если она существует в базе данных
                command.CommandText = "DROP TABLE IF EXISTS cpumetrics";
                // отправляем запрос в базу данных
                command.ExecuteNonQuery();

                
                command.CommandText = @"CREATE TABLE cpumetrics(id INTEGER PRIMARY KEY,
                    value INT, time INT)";

                command.CommandText = "DROP TABLE IF EXISTS rammetrics";
                command.ExecuteNonQuery();
                
                command.CommandText = @"CREATE TABLE rammetrics(id INTEGER PRIMARY KEY,
                    value INT, time INT)";
                command.ExecuteNonQuery();

                command.CommandText = "DROP TABLE IF EXISTS networkmetric";
                command.ExecuteNonQuery();
                
                command.CommandText = @"CREATE TABLE networkmetric(id INTEGER PRIMARY KEY,
                    value INT, time INT)";
                command.ExecuteNonQuery();
                
                command.CommandText = "DROP TABLE IF EXISTS hddmetric";
                command.ExecuteNonQuery();
                
                command.CommandText = @"CREATE TABLE hddmetric(id INTEGER PRIMARY KEY ,VALUE INT ,time INT)";
                command.ExecuteNonQuery();
              
                command.CommandText = "DROP TABLE IF EXISTS dotnetmetric";
                command.ExecuteNonQuery();
                
                command.CommandText = @"CREATE TABLE dotnetmetric(id INTEGER PRIMARY KEY ,VALUE INT ,time INT)";
                command.ExecuteNonQuery();
                
                command.CommandText = "DROP TABLE IF EXISTS cpumetric";
                command.ExecuteNonQuery();
                
                command.CommandText = @"CREATE TABLE cpumetric(id INTEGER PRIMARY KEY ,VALUE INT ,time INT)";
                command.ExecuteNonQuery();
                
            }
        }
        
        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}