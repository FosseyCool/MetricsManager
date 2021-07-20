using System;

namespace MetricsManager.Requests
{
    public class NetworkMetricsApiRequest
    {
        public Uri AgentUrl { get; set; }
        public DateTimeOffset FromTime { get; set; } 
        public DateTimeOffset ToTime { get; set; }

    }
}