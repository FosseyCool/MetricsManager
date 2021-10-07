using System.Collections.Generic;
using Core.Intefaces;
using MetricsAgent.Controllers;
using MetricsAgent.DAL.Responses;

namespace MetricsAgent.DAL.Interfaces
{
    public interface ICpuMetricsRepository : IRepository<CpuMetric>
    {
        IList<CpuMetric> GetAll();
    }
}