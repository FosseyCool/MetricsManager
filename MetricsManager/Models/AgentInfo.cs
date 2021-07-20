using System;

namespace MetricsManager.Models
{
    public class AgentInfo
    {
        public int AgentId { get; set; }

        public Uri AgentUrl { get; set; }
        
        public bool IsEnabled { get; set; }

    }
}