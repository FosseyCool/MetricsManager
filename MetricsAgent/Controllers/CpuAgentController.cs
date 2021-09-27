using System;
using System.Collections.Generic;
using AutoMapper;
using MetricsAgent.DAL;
using MetricsAgent.DAL.Interfaces;
using MetricsAgent.DAL.Responses;
using MetricsAgent.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using CpuMetric = MetricsAgent.DAL.Responses.CpuMetric;

namespace MetricsAgent.Controllers
{
    [Route("api/metrics/cpu")]
    [ApiController]
    public class CpuAgentController : Controller
    {
        private readonly ILogger<CpuAgentController> _logger;
        private ICpuMetricsRepository _repository;
        
        private readonly IMapper _mapper;

        

        public CpuAgentController(ILogger<CpuAgentController> logger,ICpuMetricsRepository repository,IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
            
            _logger = logger;
            _logger.LogDebug(1, "NLog встроен в CpuAgentController");
        }
        
        [HttpPost("create")]

        public IActionResult Create([FromBody] CpuMetric metric)
        {
           _repository.Create(metric);
            return Ok();
        }
            
            
            
        [HttpGet("from/{fromTime}/to/{toTime}")]
        public IActionResult GetMetricsCpu([FromRoute] DateTimeOffset fromTime, [FromRoute] DateTimeOffset toTime)
        {
            _logger.LogInformation("Привет! Это наше первое сообщение в лог");

            IList<CpuMetric> metrics = _repository.GetByTimePeriod(fromTime, toTime);

            CpuMetricResponse response = new CpuMetricResponse()
            {
                Metrics = new List<CpuMetric>()
            };

            foreach (var metric in metrics)
            {
                response.Metrics.Add(_mapper.Map<CpuMetric>(metric));
                    
            }

            return Ok(response);
        }
        
        
        [HttpGet ("all")]
        public IActionResult GetAll()
        {
            
            IList<CpuMetric> metrics = _repository.GetAll();

            var response = new CpuMetricResponse()
            {
                Metrics = new List<CpuMetric>()
            };

            foreach (var metric in metrics)
            {
                response.Metrics.Add(_mapper.Map<CpuMetric>(metric));
            }

            return Ok(response);
        }
    }
    
}