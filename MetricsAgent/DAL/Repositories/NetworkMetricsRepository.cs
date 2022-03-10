using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using Core.Intefaces;
using Dapper;
using MetricsAgent.DAL.Interfaces;
using MetricsAgent.DAL.Responses;
using MetricsAgent.Models;

namespace MetricsAgent.DAL
{

    public class NetworkMetricsRepository:INetworkMetricsRepository
    {
        private const string ConnectionString = "Data Source=metrics.db;Version=3;Pooling=true;Max Pool Size=100;";
        
        private readonly ConnectionManager _manager = new ConnectionManager();
        public NetworkMetricsRepository()
        {
            SqlMapper.AddTypeHandler(new DateTimeHandler());
        }


        public void Create(NetworkMetric item)
        {
            using var connection = _manager.GetOpenedConnection();
            {
                connection.Execute("INSERT INTO networkmetric(value, time) VALUES(@value, @time)",
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
                connection.Execute("DELETE FROM networkmetric WHERE id=@id",
                    new
                    {
                        id = id
                    });

            }
        }

        public void Update(Responses.NetworkMetric item)
        {
            using (var connection = new SQLiteConnection(ConnectionString))
            {
                connection.Execute("UPDATE networkmetric SET value = @value, time = @time WHERE id=@id",
                    new
                    {
                        value = item.Value,
                        time = item.Time.ToUnixTimeSeconds(),
                        id = item.Id
                    });
            }

        }
        
        public IList<Responses.NetworkMetric> GetAll()
        {
            using (var connection = new SQLiteConnection(ConnectionString))
            {
                return connection.Query<Responses.NetworkMetric>("SELECT Id, Time, Value FROM networkmetric").ToList();
            }
        }

        public NetworkMetric GetById(int id)
        {
            using (var connection = new SQLiteConnection(ConnectionString))
            {
                return connection.QuerySingle<NetworkMetric>("SELECT Id, Time, Value FROM networkmetric WHERE id=@id",
                    new {id = id});
            }
        }
        List<NetworkMetric> IRepository<NetworkMetric>.GetByTimePeriod(DateTimeOffset fromTime, DateTimeOffset toTime)
        {
            using var connection = _manager.GetOpenedConnection();
            {
                return connection.Query<NetworkMetric>("SELECT * FROM networkmetric WHERE time BETWEEN @fromTime AND @toTime",
                    new
                    {
                        fromTime = fromTime.ToUnixTimeSeconds(),
                        toTime = toTime.ToUnixTimeSeconds()
                    }).ToList();
            }
          
        }
        
       
    }
}