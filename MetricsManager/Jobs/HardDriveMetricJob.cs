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
    public class HardDriveMetricJob : IJob
    {
        private readonly IHardDriveMetricRepository _repository;
        private readonly IAgentRepository _agent;
        private readonly ILogger<HardDriveMetricJob> _logger;
        private readonly IMetricsAgentClient _client;
        private readonly IMapper _mapper;

        public HardDriveMetricJob(
            IHardDriveMetricRepository repository,
            IAgentRepository agent,
            ILogger<HardDriveMetricJob> logger,
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

            foreach (var agents in activeAgents)
            {
                await RetrieveMetricsFromAgent(agents);
            }
        }

        private async Task RetrieveMetricsFromAgent(AgentInfo agent)
        {
            try
            {
                DateTimeOffset lastDate = _repository.GetLastDateFromAgent(agent.AgentId);

                var response = await _client.GetAllHardDriveMetrics(new HardDriveMetricsApiRequest
                {
                    AgentUrl = agent.AgentUrl,
                    FromTime = lastDate,
                    ToTime = DateTimeOffset.Now
                });

                if (response == null) return;

                foreach (var hddMetric in response.Metrics.Select(metric => _mapper.Map<HardDriveMetric>(metric)))
                {
                    hddMetric.AgentId = agent.AgentId;
                    _repository.Create(hddMetric);
                }


            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }
        }
    }
}