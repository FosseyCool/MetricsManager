using System;

namespace MetricsAgent.DAL.Responses
{
    public class CpuMetric
    {
         public int Id { get; set; }
         
         public int Value { get; set; }
         
         public DateTimeOffset Time { get; set; }

    }
}