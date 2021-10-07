using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using Core.Intefaces;
using Dapper;
using MetricsAgent.DAL.Interfaces;
using MetricsAgent.Models;
using DotNetMetric = MetricsAgent.DAL.Responses.DotNetMetric;

namespace MetricsAgent.DAL
{
  
    public class DotNetRepository:IDotNetRepository
    {
        private const string ConnectionString="Data Source=metrics.db;Version=3;Pooling=true;Max Pool Size=100;";
        
        private readonly ConnectionManager _manager = new ConnectionManager();

        public DotNetRepository()
        {
            SqlMapper.AddTypeHandler(new DateTimeHandler());
        }

        public void Create(Responses.DotNetMetric item)
        {
            using var connection = _manager.GetOpenedConnection();
            {
                connection.Execute("INSERT INTO dotnetmetric(value, time) VALUES(@value, @time)",
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
                connection.Execute("DELETE FROM dotnetmetric WHERE id=@id",
                    new
                    {
                        id = id
                    });

            }
        }

        public void Update(Responses.DotNetMetric item)
        {
            using (var connection = new SQLiteConnection(ConnectionString))
            {
                connection.Execute("UPDATE dotnetmetric SET value = @value, time = @time WHERE id=@id",
                    new
                    {
                        value = item.Value,
                        time = item.Time.ToUnixTimeSeconds(),
                        id = item.Id
                    });
            }

        }
        
        public IList<Responses.DotNetMetric> GetAll()
        {
            using (var connection = new SQLiteConnection(ConnectionString))
            {
                return connection.Query<Responses.DotNetMetric>("SELECT Id, Time, Value FROM dotnetmetric").ToList();
            }
        }

        public DotNetMetric GetById(int id)
        {
            using (var connection = new SQLiteConnection(ConnectionString))
            {
                return connection.QuerySingle<DotNetMetric>("SELECT Id, Time, Value FROM dotnetmetric WHERE id=@id",
                    new {id = id});
            }
        }


        List<Responses.DotNetMetric> IRepository<Responses.DotNetMetric>.GetByTimePeriod(DateTimeOffset fromTime, DateTimeOffset toTime)
        {
            using var connection = _manager.GetOpenedConnection();
            {
                return connection.Query<DotNetMetric>("SELECT * FROM dotnetmetric WHERE BETWEEN @fromTime AND @toTime ", new
                {
                    fromTime = fromTime.ToUnixTimeSeconds(),
                    toTime = toTime.ToUnixTimeSeconds()
                }).ToList();
            }

        }
    }
}