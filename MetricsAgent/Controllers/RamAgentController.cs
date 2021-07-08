using System;
using System.Collections.Generic;
using System.Data.SQLite;
using AutoMapper;
using MetricsAgent.DAL;
using MetricsAgent.DAL.Interfaces;
using MetricsAgent.DAL.Responses;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NLog;


namespace MetricsAgent.Controllers
{
    [Route("api/ram/available")]
    [ApiController]
    public class RamAgentController : Controller
    {
        private IRamMetricRepository _repository;
        private readonly IMapper _mapper;
        
        private readonly ILogger<RamAgentController> _logger;
        public RamAgentController(ILogger<RamAgentController> logger,IRamMetricRepository repository,IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
            _logger = logger;
            _logger.LogDebug(1,"NLog встроен в RamAgentController");
        }

        [HttpPost("create")]
        public IActionResult Create([FromBody] RamMetric metric)
        {
            _repository.Create(metric);
            return Ok();
        }

        [HttpGet("all")]
        public IActionResult GetAll()
        {
            IList<DAL.Responses.RamMetric> metrics = _repository.GetAll();
            var response = new RamMetricRespose()
            {
                Metrics = new List<DAL.Responses.RamMetric>()
            };

            foreach (var metric in metrics)
            {
                    response.Metrics.Add(
                        new DAL.Responses.RamMetric
                        {
                            Time = metric.Time,
                            Value = metric.Value,
                            Id = metric.Id
                        }
                        );
            }

            return Ok(response);

        }
       

        [HttpGet("from/{fromTime}/to/{toTime}")]
        public IActionResult GetMetricsRam([FromRoute] DateTimeOffset fromTime, [FromRoute] DateTimeOffset toTime)
        {
            _logger.LogInformation("Привет! Это наше первое сообщение в лог");
            
            IList<RamMetric> metrics = _repository.GetByTimePeriod(fromTime, toTime);

            RamMetricRespose response = new RamMetricRespose()
            {
                Metrics = new List<RamMetric>()
            };

            foreach (var metric in metrics)
            {
                response.Metrics.Add(_mapper.Map<RamMetric>(metric));
            }

            return Ok(response);
        }

        
    }

   
}