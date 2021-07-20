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
using ILogger = NLog.ILogger;

namespace MetricsManager.Controllers
{
   [ApiController]
    [Route("api/metrics/dotnet")]
    public class DotNetMetricsController : Controller
    {
        private readonly ILogger<DotNetMetricsController> _logger;
        private readonly IDotNetMetricRepository _repository;
        private readonly IMapper _mapper;

        public DotNetMetricsController(
            ILogger<DotNetMetricsController> logger,
            IDotNetMetricRepository repository,
            IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
            _logger = logger;
            _logger.LogDebug(1, "NLog встроен в DotNetMetricsController");
        }

        [HttpGet("agent/{agentId}/from/{fromTime}/to/{toTime}")]
        public IActionResult GetMetricsFromAgent(
            [FromRoute] int agentId, 
            [FromRoute] DateTimeOffset fromTime, 
            [FromRoute] DateTimeOffset toTime)
        {
            _logger.LogInformation($"Input: agentId: {agentId}; fromTime - {fromTime}; toTime - {toTime}");
            
            IList<DotNetMetric> metrics = _repository.GetMetricsFromAgent(agentId, fromTime, toTime);
            AllDotNetMetricsResponses response = new AllDotNetMetricsResponses()
            {
                Metrics = new List<DotNetMetricDto>()
            };

            foreach (var metric in metrics)
            {
                response.Metrics.Add(_mapper.Map<DotNetMetricDto>(metric));
            }

            return Ok(response);
        }

        [HttpGet("cluster/from/{fromTime}/to/{toTime}")]
        public IActionResult GetMetricsFromCluster([FromRoute] DateTimeOffset fromTime, [FromRoute] DateTimeOffset toTime)
        {
            _logger.LogInformation($"Input: fromTime - {fromTime}; toTime - {toTime}");
            
            IList<DotNetMetric> metrics = _repository.GetByTimePeriod(fromTime, toTime);
            AllDotNetMetricsResponses response = new AllDotNetMetricsResponses()
            {
                Metrics = new List<DotNetMetricDto>()
            };

            foreach (var metric in metrics)
            {
                response.Metrics.Add(_mapper.Map<DotNetMetricDto>(metric));
            }

            return Ok(response);
        }
    }
}