using System;
using System.Diagnostics;
using System.Threading.Tasks;
using MetricsAgent.DAL.Interfaces;
using MetricsAgent.DAL.Responses;
using Quartz;

namespace MetricsAgent.Jobs
{
    public class NetworkJob:IJob
    {
        private INetworkMetricsRepository _repository;
        
        // счетчик для метрики CPU
        private PerformanceCounter _networkCounter;

        public NetworkJob(INetworkMetricsRepository repository)
        {
            _repository = repository;
            _networkCounter = new PerformanceCounter("Network Interface", "Bytes Total/sec", "Intel[R] Dual Band Wireless-AC 8260");
        }
        
        public Task Execute(IJobExecutionContext context)
        {
          
            var networkUssage = Convert.ToInt32(_networkCounter.NextValue());

           
            var time = DateTimeOffset.UtcNow;

            

            _repository.Create(new NetworkMetric() { Time = time , Value = networkUssage });
            
            return Task.CompletedTask;
        }
    }
}