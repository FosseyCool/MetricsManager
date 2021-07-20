using System;

namespace MetricsManager.Requests
{
    public class HardDriveMetricsApiRequest
    { 
        public Uri AgentUrl { get; set; }
        public DateTimeOffset FromTime { get; set; } 
        public DateTimeOffset ToTime { get; set; }
    }
}