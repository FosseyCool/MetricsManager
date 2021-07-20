using System;

namespace MetricsManager.Requests
{
    public class RamMetricsApiRequest
    {
        public Uri AgentUrl { get; set; } 
        public DateTimeOffset FromTime { get; set; }
        public DateTimeOffset ToTime { get; set; }

    }
}