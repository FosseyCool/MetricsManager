using System;
using System.Collections.Generic;
using System.Linq;
using Dapper;
using MetricsManager.DAL.Interfaces;
using MetricsManager.Models;

namespace MetricsManager.DAL.Repositories
{
       public class CpuMetricRepository : ICpuMetricRepository
    {
        private ConnectionManager _connection = new ConnectionManager();
        
        public List<CpuMetric> GetByTimePeriod(DateTimeOffset fromTime, DateTimeOffset toTime)
        {
            using var connection = _connection.GetOpenedConnection();
            
            return connection.Query<CpuMetric>(
                "SELECT * FROM cpumetrics WHERE time BETWEEN @fromTime AND @toTime",
                new
                {
                    fromTime = fromTime.ToUnixTimeSeconds(),
                    toTime = toTime.ToUnixTimeSeconds()
                }).ToList();
        }

        public IList<CpuMetric> GetMetricsFromAgent(int id, DateTimeOffset fromTime, DateTimeOffset toTime)
        {
            using var connection = _connection.GetOpenedConnection();
            
            return connection.Query<CpuMetric>(
                "SELECT * FROM cpumetrics WHERE AgentId = @agentId AND time BETWEEN @fromTime AND @toTime",
                new
                {
                    agentId = id,
                    fromTime = fromTime.ToUnixTimeSeconds(),
                    toTime = toTime.ToUnixTimeSeconds()
                }).ToList();
        }

        public DateTimeOffset GetLastDateFromAgent(int agentId)
        {
            using var connection = _connection.GetOpenedConnection();

            return connection.ExecuteScalar<DateTimeOffset>("SELECT MAX(Time) FROM cpumetrics WHERE AgentId = @AgentId",
                new
                {
                    AgentId = agentId
                });
        }

        public void Create(CpuMetric item)
        {
            using var connection = _connection.GetOpenedConnection();

            connection.Execute("INSERT INTO cpumetrics(agentId ,value, time) VALUES(@agentId, @value, @time)",
                new
                {
                    agentId = item.AgentId,
                    value = item.Value,
                    time = item.Time.ToUnixTimeSeconds()
                });
        }
    }
}