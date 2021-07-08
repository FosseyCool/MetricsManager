using System.Collections.Generic;

namespace MetricsAgent.DAL.Responses
{
    public class CpuMetricResponse
    {
        public List<CpuMetric> Metrics { get; set; }

        public CpuMetricResponse()
        {
            Metrics = new List<CpuMetric>();
        }
    }
}