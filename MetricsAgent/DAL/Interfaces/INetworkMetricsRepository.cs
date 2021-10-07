using System.Collections.Generic;
using Core.Intefaces;
using MetricsAgent.DAL.Responses;
using MetricsAgent.Models;

namespace MetricsAgent.DAL.Interfaces
{
    public interface INetworkMetricsRepository : IRepository<NetworkMetric>
    {
        IList<NetworkMetric> GetAll();
    }
}