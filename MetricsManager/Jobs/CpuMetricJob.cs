

using System;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using MetricsManager.Client;
using MetricsManager.DAL.Interfaces;
using MetricsManager.Models;
using MetricsManager.Requests;
using Microsoft.Extensions.Logging;
using Quartz;

namespace MetricsManager.Jobs
{
    [DisallowConcurrentExecution]
    public class CpuMetricJob : IJob
    {
        private readonly ICpuMetricRepository _repository;
        private readonly IAgentRepository _agent;
        private readonly ILogger<CpuMetricJob> _logger;
        private readonly IMetricsAgentClient _client;
        private readonly IMapper _mapper;

        public CpuMetricJob(
            ICpuMetricRepository repository,
            IAgentRepository agent,
            ILogger<CpuMetricJob> logger,
            IMetricsAgentClient client,
            IMapper mapper)
        {
            _repository = repository;
            _agent = agent;
            _logger = logger;
            _client = client;
            _mapper = mapper;
        }
        
        public async Task Execute(IJobExecutionContext context)
        {
            var activeAgents = _agent.GetRegisteredList().Where(x => x.IsEnabled);

            foreach (var agent in activeAgents)
            {
                await RetrieveMetricsFromAgent(agent);
            }
        }

        private async Task RetrieveMetricsFromAgent(AgentInfo agent)
        {
            try
            {
                DateTimeOffset lastDate = _repository.GetLastDateFromAgent(agent.AgentId);
                var response = await _client.GetAllCpuMetrics(new CpuMetricsApiRequest
                {
                    AgentUrl = agent.AgentUrl,
                    FromTime = lastDate,
                    ToTime = DateTimeOffset.Now
                    
                });

                if (response == null) return;

                foreach (var cpuMetric in response.Metrics.Select(metric => _mapper.Map<CpuMetric>(metric)))
                {
                    cpuMetric.AgentId = agent.AgentId;
                    _repository.Create(cpuMetric);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }
        }
        
    }
}