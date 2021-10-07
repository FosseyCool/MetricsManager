using System.Collections.Generic;

namespace MetricsAgent.DAL.Responses
{
    public class RamMetricRespose
    {
        public List<RamMetric> Metrics { get; set; }

        public RamMetricRespose()
        {
            Metrics = new List<RamMetric>();
        }
    }
}