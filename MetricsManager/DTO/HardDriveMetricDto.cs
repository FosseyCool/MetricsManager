using System;

namespace MetricsManager.DTO
{
    public class HardDriveMetricDto
    {
         public int Value { get; set; } 
         public int AgentId { get; set; }
         public DateTimeOffset Time { get; set; }
    }
}