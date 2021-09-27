using System.Collections.Generic;

namespace MetricsAgent.DAL.Responses
{
    public class HddMetricResponse
    {
        public List<HddMetric> Metrics { get; set; }

        public HddMetricResponse()
        {
            Metrics = new List<HddMetric>();
        }
    }
}