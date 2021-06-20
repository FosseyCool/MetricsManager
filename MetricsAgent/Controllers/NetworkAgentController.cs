using System;
using MetricsAgent.DAL;
using MetricsAgent.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NLog;
using ILogger = NLog.ILogger;

namespace MetricsAgent.Controllers
{
    [Route("api/network")]
    [ApiController]
    public class NetworkAgentController : Controller
    {
        private INetworkMetricsRepository repository;
        
        private readonly ILogger<NetworkAgentController> _logger;

        public NetworkAgentController(ILogger<NetworkAgentController> logger,INetworkMetricsRepository repository)
        {
            this.repository = repository;
            _logger = logger;
            _logger.LogDebug(1,"NLog встроен в NetworkAgentController");
        }
        [HttpPost("create")]
        public IActionResult Create([FromBody] NetworkMetric request)
        {
            repository.Create(new NetWorkMetric
            {  
                Time = DateTimeOffset.FromUnixTimeSeconds(request.Time),
                Value = request.Value

            });
            return Ok();
        }
        
        [HttpGet("from/{fromTime}/to/{toTime}")]
        public IActionResult GetMetricsNetwork([FromRoute] TimeSpan fromTime, [FromRoute] TimeSpan toTime)
        {
            _logger.LogInformation("Привет! Это наше первое сообщение в лог");
            return Ok();
        }
    }
    
    public class NetworkMetric
    {
        public int Id { get; set; }

        public int Value { get; set; }

        public long Time { get; set; }
        
    }
}