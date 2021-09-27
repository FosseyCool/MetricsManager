using System.Collections.Generic;

namespace MetricsAgent.DAL.Responses
{
    public class DotNetMetricResponse
    {
        public List<DotNetMetric> Metrics { get; set; } 

        public DotNetMetricResponse()
        {
            Metrics = new List<DotNetMetric>();
        }
    }
}