using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using Core.Intefaces;
using Dapper;
using MetricsAgent.DAL.Interfaces;
using MetricsAgent.Models;
using HddMetric = MetricsAgent.DAL.Responses.HddMetric;

namespace MetricsAgent.DAL
{
  
    public class HddMetricsRepository:IHddMetricsRepository
    {
        private const string ConnectionString="Data Source=metrics.db;Version=3;Pooling=true;Max Pool Size=100;";

        private readonly ConnectionManager _manager = new ConnectionManager();
        public HddMetricsRepository()
        {
            SqlMapper.AddTypeHandler(new DateTimeHandler());
        }
        
     

        public void Create(Responses.HddMetric item)
        {
            using var connection = _manager.GetOpenedConnection();
            {
                connection.Execute("INSERT INTO hddmetric(value, time) VALUES(@value, @time)",
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
                connection.Execute("DELETE FROM hddmetric WHERE id=@id",
                    new
                    {
                        id = id
                    });

            }
        }

        public void Update(Responses.HddMetric item)
        {
            using (var connection = new SQLiteConnection(ConnectionString))
            {
                connection.Execute("UPDATE hddmetric SET value = @value, time = @time WHERE id=@id",
                    new
                    {
                        value = item.Value,
                        time = item.Time.ToUnixTimeSeconds(),
                        id = item.Id
                    });
            }

        }
        
        public IList<Responses.HddMetric> GetAll()
        {
            using (var connection = new SQLiteConnection(ConnectionString))
            {
                return connection.Query<Responses.HddMetric>("SELECT Id, Time, Value FROM hddmetric").ToList();
            }
        }

        public HddMetric GetById(int id)
        {
            using (var connection = new SQLiteConnection(ConnectionString))
            {
                return connection.QuerySingle<HddMetric>("SELECT Id, Time, Value FROM hddmetric WHERE id=@id",
                    new {id = id});
            }
        }


        List<Responses.HddMetric> IRepository<Responses.HddMetric>.GetByTimePeriod(DateTimeOffset fromTime, DateTimeOffset toTime)
        {
            using var connection = _manager.GetOpenedConnection();
            {
                return connection.Query<HddMetric>("SELECT * FROM hddmetric WHERE BETWEEN @fromTime AND @toTime ", new
                {
                    fromTime = fromTime.ToUnixTimeSeconds(),
                    toTime = toTime.ToUnixTimeSeconds()
                }).ToList();
            }
        }
    }
}