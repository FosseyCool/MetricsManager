using System;
using System.Diagnostics;
using System.Threading.Tasks;
using MetricsAgent.DAL.Interfaces;
using MetricsAgent.DAL.Responses;
using Quartz;

namespace MetricsAgent.Jobs
{
    public class HddMetricJob:IJob
    {
        private IHddMetricsRepository _repository;

        private PerformanceCounter _hddCounter;

        public HddMetricJob(IHddMetricsRepository repository)
        {
            _repository = repository;
            _hddCounter = new PerformanceCounter("LogicalDisk", "% Free Space", "_Total");
        }
        
        public Task Execute(IJobExecutionContext context)
        {
            // получаем значение занятости CPU
            var hddUsage = Convert.ToInt32(_hddCounter.NextValue());

            // узнаем когда мы сняли значение метрики.
            var time = DateTimeOffset.UtcNow;

            // теперь можно записать что-то при помощи репозитория

            _repository.Create(new HddMetric() { Time = time , Value = hddUsage });
            
            return Task.CompletedTask;
        }
    }
}