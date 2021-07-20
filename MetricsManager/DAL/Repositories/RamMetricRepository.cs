using System;
using System.Collections.Generic;
using System.Linq;
using Dapper;
using MetricsManager.DAL.Interfaces;
using MetricsManager.Models;

namespace MetricsManager.DAL.Repositories
{
    public class RamMetricRepository : IRamMetricRepository
    {
        private ConnectionManager _connection = new ConnectionManager();
        
        public List<RamMetric> GetByTimePeriod(DateTimeOffset fromTime, DateTimeOffset toTime)
        {
            using var connection = _connection.GetOpenedConnection();

            return connection.Query<RamMetric>(
                "SELECT * FROM rammetrics WHERE Time BETWEEN @FromTime AND @ToTime",
                new
                {
                    FromTime = fromTime.ToUnixTimeSeconds(),
                    ToTime = toTime.ToUnixTimeSeconds()
                }).ToList();
        }

        public void Create(RamMetric item)
        {
            using var connection = _connection.GetOpenedConnection();

            connection.Execute("INSERT INTO rammetrics(AgentId, Value, Time) VALUES(@agentId, @value, @time)",
                new
                {
                    agentId = item.AgentId,
                    value = item.Value,
                    time = item.Time.ToUnixTimeSeconds()
                });
        }

        public IList<RamMetric> GetMetricsFromAgent(int id, DateTimeOffset fromTime, DateTimeOffset toTime)
        {
            using var connection = _connection.GetOpenedConnection();

            return connection.Query<RamMetric>(
                "SELECT * FROM rammetrics WHERE AgentId = @agentId AND Time BETWEEN @FromTime AND @ToTime",
                new
                {
                    agentId = id,
                    FromTime = fromTime.ToUnixTimeSeconds(),
                    ToTime = toTime.ToUnixTimeSeconds()
                }).ToList();
        }

        public DateTimeOffset GetLastDateFromAgent(int agentId)
        {
            using var connection = _connection.GetOpenedConnection();

            return connection.ExecuteScalar<DateTimeOffset>("SELECT MAX(Time) FROM rammetrics WHERE AgentId = @AgentId",
                new
                {
                    AgentId = agentId
                });
        }
    }
}