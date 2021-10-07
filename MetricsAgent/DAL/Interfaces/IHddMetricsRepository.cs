using System.Collections.Generic;
using Core.Intefaces;
using MetricsAgent.DAL.Responses;

namespace MetricsAgent.DAL.Interfaces
{
    public interface IHddMetricsRepository : IRepository<HddMetric>
    {
        IList<HddMetric> GetAll();
    }
}