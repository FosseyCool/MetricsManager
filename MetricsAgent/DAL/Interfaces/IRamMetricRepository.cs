using System.Collections.Generic;
using Core.Intefaces;
using MetricsAgent.DAL.Responses;

namespace MetricsAgent.DAL.Interfaces
{
    public interface IRamMetricRepository : IRepository<RamMetric>
    {
        IList<RamMetric> GetAll();
    }
}