using System;
using MetricsAgent.DAL;
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
        private IDotNetRepository repository;

        public DotNetAgentController(ILogger<DotNetAgentController> logger,IDotNetRepository repository)
        {
            this.repository = repository;
            _logger = logger;
            _logger.LogDebug(1,"NLog встроен в DotNetAgentController");
        }
        [HttpPost("create")]
        public IActionResult Create([FromBody] DotNetMetric request)
        {
            repository.Create(new Models.DotNetMetric()
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
    public class DotNetMetric
    {
        public int Id { get; set; }

        public int Value { get; set; }

        public long Time { get; set; }
        
    }
}