using System.Collections.Generic;
using MetricsManager.DTO;

namespace MetricsManager.Responses
{
    public class AllCpuMetricResponses
    {
        public List<CpuMetricDto> Metrics { get; set; }

        public AllCpuMetricResponses()
        {
            Metrics = new List<CpuMetricDto>();
        }
    }
}