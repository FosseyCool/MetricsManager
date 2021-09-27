using System.Collections.Generic;
using MetricsManager.DTO;

namespace MetricsManager.Responses
{
    public class AllHardDriveMetricsResponses
    {
        public List<HardDriveMetricDto> Metrics { get; set; }

        public AllHardDriveMetricsResponses()
        {
            Metrics = new List<HardDriveMetricDto>();
        }
    }
}