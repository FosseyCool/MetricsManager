using System.Collections.Generic;

namespace MetricsAgent.DAL.Responses
{
    public class NetworkMetricResponse
    {
        public List<NetworkMetric> Metrics { get; set; }

        public NetworkMetricResponse()
        {
            Metrics = new List<NetworkMetric>();
        }
    }
}