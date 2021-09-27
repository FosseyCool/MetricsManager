using System.Collections.Generic;
using MetricsManager.DTO;

namespace MetricsManager.Responses
{
    public class AllNetworkMetricsResponses
    {
        public List<NetworkMetricDto> Metrics { get; set; }

        public AllNetworkMetricsResponses()
        {
            Metrics = new List<NetworkMetricDto>();
        }
    }
}