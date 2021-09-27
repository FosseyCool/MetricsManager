

using System.Threading.Tasks;
using MetricsManager.Requests;
using MetricsManager.Responses;

namespace MetricsManager.Client
{
    public interface IMetricsAgentClient
    {
        Task<AllCpuMetricResponses> GetAllCpuMetrics(CpuMetricsApiRequest request);

        Task<AllDotNetMetricsResponses> GetAllDotNetMetrics(DotNetMetricsApiRequest request);

        Task<AllHardDriveMetricsResponses> GetAllHardDriveMetrics(HardDriveMetricsApiRequest request);

        Task<AllNetworkMetricsResponses> GetAllNetworkMetrics(NetworkMetricsApiRequest request);

        Task<AllRamMetricsResponses> GetAllRamMetrics(RamMetricsApiRequest request);
    }
}