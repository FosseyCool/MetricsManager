using System;
using System.Collections.Generic;
using System.Data.SQLite;
using MetricsAgent.Models;

namespace MetricsAgent.DAL
{
    
    
    public interface INetworkMetricsRepository : IRepository<NetWorkMetric>
    {
        
    }
    
    public class NetworkMetricsRepository:INetworkMetricsRepository
    {
        private const string ConnectionString = "Data Source=metrics.db;Version=3;Pooling=true;Max Pool Size=100;";
        
        public void Create(NetWorkMetric item)
        {
            using var connection = new SQLiteConnection(ConnectionString);
            connection.Open();
            

            using var cmd = new SQLiteCommand(connection);
            cmd.CommandText = "INSERT INTO networkmetric(value ,time ) VALUES (@value ,@time )";
            cmd.Parameters.AddWithValue("@value", item.Value);
            cmd.Parameters.AddWithValue("@time", item.Time.ToUnixTimeSeconds());
            
            cmd.Prepare();
            cmd.ExecuteNonQuery();

        }

        public IList<NetWorkMetric> GetByTimePeriod(DateTimeOffset fromTime, DateTimeOffset toTime)
        {
            using var connection = new SQLiteConnection(ConnectionString);
            connection.Open();

            using var cmd = new SQLiteCommand(connection) ;
            cmd.CommandText = "SELECT*FROM networkmetric WHERE time BETWEEN @fomTime AND @toTime";
            cmd.Parameters.AddWithValue("@fromTime", fromTime.ToUnixTimeSeconds());
            cmd.Parameters.AddWithValue("@toTime", toTime.ToUnixTimeSeconds());

            List<NetWorkMetric> returnList = new List<NetWorkMetric>();

            using SQLiteDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                returnList.Add(new NetWorkMetric()
                {
                    Id = reader.GetInt32(0),
                    Value = reader.GetInt32(1),
                    Time = DateTimeOffset.FromUnixTimeSeconds(reader.GetInt32(2))
                });
            }

            return returnList;

        }
    }
}