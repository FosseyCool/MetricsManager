using System.Collections.Generic;
using MetricsManager.DTO;

namespace MetricsManager.Responses
{
    public class AllDotNetMetricsResponses
    {
        public List<DotNetMetricDto> Metrics { get; set; }

        public AllDotNetMetricsResponses()
        {
            Metrics = new List<DotNetMetricDto>();
        }
    }
}