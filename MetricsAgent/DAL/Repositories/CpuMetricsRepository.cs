using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using Core.Intefaces;
using Dapper;
using MetricsAgent.DAL.Interfaces;
using MetricsAgent.Models;

namespace MetricsAgent.DAL
{
   
    public class CpuMetricsRepository:ICpuMetricsRepository
    {
        private const string ConnectionString="Data Source=metrics.db;Version=3;Pooling=true;Max Pool Size=100;";
        
        private readonly ConnectionManager _manager = new ConnectionManager();

        public CpuMetricsRepository()
        {
            SqlMapper.AddTypeHandler(new DateTimeHandler());
        }
        
 
        public void Delete(int id)
        {
            using (var connection= new SQLiteConnection(ConnectionString))
            {
                connection.Execute("DELETE FROM cpumetrics WHERE id=@id",
                    new
                    {
                        id = id
                    });

            }
        }

        public void Update(Responses.CpuMetric item)
        {
            using (var connection = new SQLiteConnection(ConnectionString))
            {
                connection.Execute("UPDATE cpumetrics SET value = @value, time = @time WHERE id=@id",
                    new
                    {
                        value = item.Value,
                        time = item.Time.ToUnixTimeSeconds(),
                        id = item.Id
                    });
            }

        }
        
        public IList<Responses.CpuMetric> GetAll()
        {
            using (var connection = new SQLiteConnection(ConnectionString))
            {
                return connection.Query<Responses.CpuMetric>("SELECT Id, Time, Value FROM cpumetric").ToList();
            }
        }

        public Responses.CpuMetric GetById(int id)
        {
            using (var connection = new SQLiteConnection(ConnectionString))
            {
                return connection.QuerySingle<Responses.CpuMetric>("SELECT Id, Time, Value FROM cpumetric WHERE id=@id",
                    new {id = id});
            }
        }

        public List<Responses.CpuMetric> GetByTimePeriod(DateTimeOffset fromTime, DateTimeOffset toTime)
        {
            using var connection = _manager.GetOpenedConnection();
            {
                return connection.Query<Responses.CpuMetric>("SELECT * FROM cpumetric WHERE time BETWEEN @fromTime AND @toTime",
                    new
                    {
                        fromTime = fromTime.ToUnixTimeSeconds(),
                        toTime = toTime.ToUnixTimeSeconds()
                    }).ToList();
            }
        }

        public void Create(Responses.CpuMetric item)
        {
            using var connection = _manager.GetOpenedConnection();
            {
                connection.Execute("INSERT INTO cpumetric(value, time) VALUES(@value, @time)",
                    // анонимный объект с параметрами запроса
                    new { 
                        // value подставится на место "@value" в строке запроса
                        // значение запишется из поля Value объекта item
                        value = item.Value,
                    
                        // записываем в поле time количество секун
                        time = item.Time.ToUnixTimeSeconds()
                    });

            }
           
        }
    }
    
    
}