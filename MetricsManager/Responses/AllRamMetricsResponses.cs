using System.Collections.Generic;
using MetricsManager.DTO;

namespace MetricsManager.Responses
{
    public class AllRamMetricsResponses
    {
        public List<RamMetricDto> Metrics { get; set; }

        public AllRamMetricsResponses()
        {
            Metrics = new List<RamMetricDto>();
        }
    }
}