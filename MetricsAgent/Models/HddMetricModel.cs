using System;

namespace MetricsAgent.Models
{
    public class HddMetricModel
    {
        public int Id { get; set; }
        public int Value { get; set; }
        public DateTimeOffset Time { get; set; }
    }
}