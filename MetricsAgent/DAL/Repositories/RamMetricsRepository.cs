using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using Core.Intefaces;
using Dapper;
using MetricsAgent.DAL.Interfaces;
using MetricsAgent.DAL.Responses;
using MetricsAgent.Models;
using RamMetric = MetricsAgent.DAL.Responses.RamMetric;

namespace MetricsAgent.DAL
{
    public class RamMetricsRepository:IRamMetricRepository
    {
        
        private const string ConnectionString = "Data Source=metrics.db;Version=3;Pooling=true;Max Pool Size=100;";
        
        private readonly ConnectionManager _manager = new ConnectionManager();
        public RamMetricsRepository()
        {
            SqlMapper.AddTypeHandler(new DateTimeHandler());
        }

     

        public void Create(Responses.RamMetric item)
        {
            using var connection = _manager.GetOpenedConnection();
            {
                connection.Execute("INSERT INTO rammetrics(value, time) VALUES(@value, @time)",
                    new
                    {
                        value = item.Value,
                        time = item.Time.ToUnixTimeSeconds()
                    });
            }

        }
        
        public void Delete(int id)
        {
            using (var connection= new SQLiteConnection(ConnectionString))
            {
                connection.Execute("DELETE FROM rammetrics WHERE id=@id",
                    new
                    {
                        id = id
                    });

            }
        }

        public void Update(Responses.RamMetric item)
        {
            using (var connection = new SQLiteConnection(ConnectionString))
            {
                connection.Execute("UPDATE rammetrics SET value = @value, time = @time WHERE id=@id",
                    new
                    {
                        value = item.Value,
                        time = item.Time.ToUnixTimeSeconds(),
                        id = item.Id
                    });
            }

        }
        
        public IList<Responses.RamMetric> GetAll()
        {
            using (var connection = new SQLiteConnection(ConnectionString))
            {
                return connection.Query<Responses.RamMetric>("SELECT Id, Time, Value FROM rammetrics").ToList();
            }
        }

        public RamMetric GetById(int id)
        {
            using (var connection = new SQLiteConnection(ConnectionString))
            {
                return connection.QuerySingle<RamMetric>("SELECT Id, Time, Value FROM rammetrics WHERE id=@id",
                    new {id = id});
            }
        }

        List<Responses.RamMetric> IRepository<Responses.RamMetric>.GetByTimePeriod(DateTimeOffset fromTime, DateTimeOffset toTime)
        {
            using var connection = _manager.GetOpenedConnection();
            {
                return connection.Query<RamMetric>("SELECT * FROM rammetrics WHERE time BETWEEN @fromTime AND @toTime",
                    new
                    {
                        fromTime = fromTime.ToUnixTimeSeconds(),
                        toTime = toTime.ToUnixTimeSeconds()
                    }).ToList();
            }
        }
    }
    
   
}