using System.Collections.Generic;
using Core.Intefaces;
using MetricsAgent.Controllers;
using MetricsAgent.DAL.Responses;

namespace MetricsAgent.DAL.Interfaces
{
    public interface IDotNetRepository : IRepository<DotNetMetric>
    {
        IList<DotNetMetric> GetAll();
    }
}