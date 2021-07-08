using System;
using System.Collections.Generic;
using AutoMapper;
using MetricsAgent.DAL;
using MetricsAgent.DAL.Interfaces;
using MetricsAgent.DAL.Responses;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NLog;
using ILogger = NLog.ILogger;

namespace MetricsAgent.Controllers
{
    [Route("api/metrics/hdd/left")]
    [ApiController]
    public class HddAgentController : Controller
    {
        private readonly ILogger<HddAgentController> _logger;
        private IHddMetricsRepository _repository;
        private readonly IMapper _mapper;
        public HddAgentController(ILogger<HddAgentController> logger,IHddMetricsRepository repository,IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
            _logger = logger;
            _logger.LogDebug(1,"NLog встроен в HddAgentController");
        }
        [HttpPost("create")]
        public IActionResult Create([FromBody] HddMetric metric)
        {
           _repository.Create(metric);
           return Ok();
        }
        [HttpGet ("all")]
        public IActionResult GetAll()
        {
            
            IList<HddMetric> metrics = _repository.GetAll();

            var response = new HddMetricResponse()
            {
                Metrics = new List<HddMetric>()
            };

            foreach (var metric in metrics)
            {
                response.Metrics.Add(_mapper.Map<HddMetric>(metric));
            }

            return Ok(response);
        }      
        [HttpGet("from/{fromTime}/to/{toTime}")]
        public IActionResult GetMetricsHdd([FromRoute] DateTimeOffset fromTime, [FromRoute] DateTimeOffset toTime)
        {
            _logger.LogInformation("Привет! Это наше первое сообщение в лог");

            IList<HddMetric> metrics = _repository.GetByTimePeriod(fromTime, toTime);

            HddMetricResponse response = new HddMetricResponse()
            {
                Metrics = new List<HddMetric>()
            };

            foreach (var metric in metrics)
            {
                response.Metrics.Add(_mapper.Map<HddMetric>(metric));
            }

            return Ok(response);
        }

    }
   
}