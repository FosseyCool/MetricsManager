using System;
using System.Collections.Generic;
using Core.Intefaces;
using MetricsManager.Models;

namespace MetricsManager.DAL.Interfaces
{
    public interface ICpuMetricRepository : IRepository<CpuMetric>
    {
        IList<CpuMetric> GetMetricsFromAgent(int id, DateTimeOffset fromTime, DateTimeOffset toTime);
        DateTimeOffset GetLastDateFromAgent(int agentId);
    }
}