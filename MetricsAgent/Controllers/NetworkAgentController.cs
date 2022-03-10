﻿using System;
using System.Collections.Generic;
using AutoMapper;
using MetricsAgent.DAL;
using MetricsAgent.DAL.Interfaces;
using MetricsAgent.DAL.Responses;
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
        private INetworkMetricsRepository _repository;
        private readonly IMapper _mapper;
        
        private readonly ILogger<NetworkAgentController> _logger;

        public NetworkAgentController(ILogger<NetworkAgentController> logger,INetworkMetricsRepository repository,IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
            _logger = logger;
            _logger.LogDebug(1,"NLog встроен в NetworkAgentController");
        }
        [HttpPost("create")]
        public IActionResult Create([FromBody] NetworkMetric metric)
        {
           _repository.Create(metric);
           return Ok();
        }
        
        [HttpGet ("all")]
        public IActionResult GetAll()
        {
            
            IList<NetworkMetric> metrics = _repository.GetAll();

            var response = new NetworkMetricResponse()
            {
                Metrics = new List<NetworkMetric>()
            };

            foreach (var metric in metrics)
            {
                response.Metrics.Add(_mapper.Map<NetworkMetric>(metric));
            }

            return Ok(response);
        }    
        
        [HttpGet("from/{fromTime}/to/{toTime}")]
        public IActionResult GetMetricsNetWork([FromRoute] DateTimeOffset fromTime, [FromRoute] DateTimeOffset toTime)
        {
            _logger.LogInformation("Привет! Это наше первое сообщение в лог");

            IList<NetworkMetric> metrics = _repository.GetByTimePeriod(fromTime, toTime);

            NetworkMetricResponse response = new NetworkMetricResponse()
            {
                Metrics = new List<NetworkMetric>()
            };

            foreach (var metric in metrics)
            {
                response.Metrics.Add(_mapper.Map<NetworkMetric>(metric));
            }

            return Ok(response);
        }
    }
}