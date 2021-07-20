using System;
using System.Collections.Generic;
using AutoMapper;
using MetricsManager.DAL.Interfaces;
using MetricsManager.DTO;
using MetricsManager.Models;
using MetricsManager.Responses;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NLog;

namespace MetricsManager.Controllers
{
      [ApiController]
    [Route("api/metrics/hdd")]
    public class HardDriveMetricsController : ControllerBase
    {
        private readonly ILogger<HardDriveMetricsController> _logger;
        private readonly IHardDriveMetricRepository _repository;
        private readonly IMapper _mapper;

        public HardDriveMetricsController(
            ILogger<HardDriveMetricsController> logger,
            IHardDriveMetricRepository repository,
            IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
            _logger = logger;
            _logger.LogDebug(1,"NLog встроен в HardDriveMetricsController");
        }

        [HttpGet("agent/{agentId}/from/{fromTime}/to/{toTime}")]
        public IActionResult GetMetricsFromAgent(
            [FromRoute] int agentId, 
            [FromRoute] DateTimeOffset fromTime, 
            [FromRoute] DateTimeOffset toTime)
        {
            _logger.LogInformation($"Input: agentId: {agentId}; fromTime - {fromTime}; toTime - {toTime}");

            IList<HardDriveMetric> metrics = _repository.GetMetricsFromAgent(agentId, fromTime, toTime);
            AllHardDriveMetricsResponses response = new AllHardDriveMetricsResponses()
            {
                Metrics = new List<HardDriveMetricDto>()
            };

            foreach (var metric in metrics)
            {
                response.Metrics.Add(_mapper.Map<HardDriveMetricDto>(metric));   
            }
            
            return Ok(response);
        }

        [HttpGet("cluster/from/{fromTime}/to/{toTime}")]
        public IActionResult GetMetricsFromCluster([FromRoute] DateTimeOffset fromTime, [FromRoute] DateTimeOffset toTime)
        {
            _logger.LogInformation($"Input: fromTime - {fromTime}; toTime - {toTime}");
            
            IList<HardDriveMetric> metrics = _repository.GetByTimePeriod(fromTime, toTime);
            AllHardDriveMetricsResponses response = new AllHardDriveMetricsResponses()
            {
                Metrics = new List<HardDriveMetricDto>()
            };

            foreach (var metric in metrics)
            {
                response.Metrics.Add(_mapper.Map<HardDriveMetricDto>(metric));   
            }
            
            return Ok(response);
        }
    }
}