using System;
using System.Diagnostics;
using System.Threading.Tasks;
using MetricsAgent.DAL.Interfaces;
using MetricsAgent.DAL.Responses;
using Quartz;

namespace MetricsAgent.Jobs
{
    public class RamMetricJob:IJob
    {
        private IRamMetricRepository _repository;

        private PerformanceCounter _ramCounter;

        public RamMetricJob(IRamMetricRepository repository)
        {
            _repository = repository;

            _ramCounter = new PerformanceCounter("Memory", "Available MBytes");
        }

        public Task Execute(IJobExecutionContext context)
        {
            var ramUsage = Convert.ToInt32(_ramCounter.NextValue());

            var time = DateTimeOffset.UtcNow;
            
            _repository.Create(new RamMetric{Time = time,Value = ramUsage});

            return Task.CompletedTask;
        }
        
    }
}