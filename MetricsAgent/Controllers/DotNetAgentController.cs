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
    [Route("api/metrics/dotnet/errors-count")]
    [ApiController]
    public class DotNetAgentController : Controller
    {
        private readonly ILogger<DotNetAgentController> _logger;
        private IDotNetRepository _repository;
        private readonly IMapper _mapper;
        public DotNetAgentController(ILogger<DotNetAgentController> logger,IDotNetRepository repository,IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
            _logger = logger;
            _logger.LogDebug(1,"NLog встроен в DotNetAgentController");
        }
        
        [HttpPost("create")]
        public IActionResult Create([FromBody] DotNetMetric metric)
        {
           _repository.Create(metric);
           return Ok();
        }
        
        [HttpGet ("all")]
        public IActionResult GetAll()
        {
            
            IList<DotNetMetric> metrics = _repository.GetAll();

            var response = new DotNetMetricResponse()
            {
                Metrics = new List<DotNetMetric>()
            };

            foreach (var metric in metrics)
            {
                response.Metrics.Add(_mapper.Map<DotNetMetric>(metric));
            }

            return Ok(response);
        }      
            
            
            
        [HttpGet("from/{fromTime}/to/{toTime}")]
        public IActionResult GetMetricsDotNet([FromRoute] DateTimeOffset fromTime, [FromRoute] DateTimeOffset toTime)
        {
            _logger.LogInformation("Привет! Это наше первое сообщение в лог");

            IList<DotNetMetric> metrics = _repository.GetByTimePeriod(fromTime, toTime);

            DotNetMetricResponse response = new DotNetMetricResponse()
            {
                Metrics = new List<DotNetMetric>()
            };

            foreach (var metric in metrics)
            {
                response.Metrics.Add(_mapper.Map<DotNetMetric>(metric));
            }

            return Ok(response);

        }
    }
   
}