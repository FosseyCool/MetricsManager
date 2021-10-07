using System;
using System.Diagnostics;
using System.Threading.Tasks;
using MetricsAgent.DAL.Interfaces;
using MetricsAgent.DAL.Responses;
using Quartz;

namespace MetricsAgent.Jobs
{
    public class DotNetMetricJob:IJob
    {
        private IDotNetRepository _repository;

        private PerformanceCounter _dotnetCounter;

        public DotNetMetricJob(IDotNetRepository repository)
        {
            _repository = repository;
            _dotnetCounter = new PerformanceCounter(".NET CLR Memory", "# Total committed Bytes", "_Global_");
            
        }
        
        public Task Execute(IJobExecutionContext context)
        {
           
            var dotnetUssage = Convert.ToInt32(_dotnetCounter.NextValue());

         
            var time = DateTimeOffset.UtcNow;

           

            _repository.Create(new DotNetMetric() { Time = time , Value = dotnetUssage });
            
            return Task.CompletedTask;
        }
    }
}