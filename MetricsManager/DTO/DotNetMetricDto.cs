using System;

namespace MetricsManager.DTO
{
    public class DotNetMetricDto
    {
        public int Value { get; set; }
        public int AgentId { get; set; }
        public DateTimeOffset Time { get; set; }
    }
}