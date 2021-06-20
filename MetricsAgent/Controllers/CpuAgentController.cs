using System;
using MetricsAgent.DAL;
using MetricsAgent.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace MetricsAgent.Controllers
{
    [Route("api/metrics/cpu")]
    [ApiController]
    public class CpuAgentController : Controller
    {
        private readonly ILogger<CpuAgentController> _logger;
        private ICpuMetricsRepository repository;
        

        public CpuAgentController(ILogger<CpuAgentController> logger,ICpuMetricsRepository repository)
        {
            this.repository = repository;
            _logger = logger;
            _logger.LogDebug(1, "NLog встроен в CpuAgentController");
        }
        
        [HttpPost("create")]

        public IActionResult Create([FromBody] CpuMetric request)
        {
            repository.Create(new Models.CpuMetric
            { 
                Time = DateTimeOffset.FromUnixTimeSeconds(request.Time),
                Value = request.Value

            });
            return Ok();
        }
            
            
            
        [HttpGet("from/{fromTime}/to/{toTime}")]
        public IActionResult GetMetricsHdd([FromRoute] DateTimeOffset fromTime, [FromRoute] DateTimeOffset toTime)
        {
            _logger.LogInformation("Привет! Это наше первое сообщение в лог");
            return Ok(repository.GetByTimePeriod(fromTime, toTime));
        }
    }
    public class CpuMetric
    {
        public int Id { get; set; }

        public int Value { get; set; }

        public long Time { get; set; }
        
    }
}