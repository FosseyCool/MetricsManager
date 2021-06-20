using System;
using MetricsAgent.DAL;
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
        private IHddMetricsRepository repository;
        //НЕ ЗАБЫТЬ 
        //СДЕЛАТЬ БАЗУ ДАННЫХ В СТАРТАПЕ
        //

        public HddAgentController(ILogger<HddAgentController> logger,IHddMetricsRepository repository)
        {
            this.repository = repository;
            _logger = logger;
            _logger.LogDebug(1,"NLog встроен в HddAgentController");
        }
        [HttpPost("create")]
        public IActionResult Create([FromBody] Hddmetric request)
        {
            repository.Create(new Models.HddMetric()
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
    public class Hddmetric
    {
        public int Id { get; set; }

        public int Value { get; set; }

        public long Time { get; set; }
        
    }
}