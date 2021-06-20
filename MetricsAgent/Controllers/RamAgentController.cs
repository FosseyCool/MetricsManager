using System;
using System.Collections.Generic;
using System.Data.SQLite;
using MetricsAgent.DAL;
using MetricsAgent.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NLog;

namespace MetricsAgent.Controllers
{
    [Route("api/ram/available")]
    [ApiController]
    public class RamAgentController : Controller
    {
        private IRamMetricRepository repository;
        private readonly ILogger<RamAgentController> _logger;
        public RamAgentController(ILogger<RamAgentController> logger,IRamMetricRepository repository)
        {
            this.repository = repository;
            _logger = logger;
            _logger.LogDebug(1,"NLog встроен в RamAgentController");
        }

        [HttpPost("create")]
        public IActionResult Create([FromBody] RamMetric request)
        {
            repository.Create(new Models.RamMetric
            {  
                Time = DateTimeOffset.FromUnixTimeSeconds(request.Time),
                Value = request.Value

            });
            return Ok();
        }

        [HttpGet("from/{fromTime}/to/{toTime}")]
        public IActionResult GetMetricsRam([FromRoute] DateTimeOffset fromTime, [FromRoute] DateTimeOffset toTime)
        {
            _logger.LogInformation("Привет! Это наше первое сообщение в лог");
            return Ok(repository.GetByTimePeriod(fromTime,toTime));
        }

        
    }
    
    public class RamMetric
    {
        public int Id { get; set; }

        public int Value { get; set; }

        public long Time { get; set; }
        
    }
  
   
}