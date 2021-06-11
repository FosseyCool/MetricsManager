using System;
using MetricsAgent.Controllers;
using Microsoft.AspNetCore.Mvc;
using Xunit;

namespace MetricsAgentTests
{
    public class CpuAgentMetricControllerUnitTests
    {
        private CpuAgentController _controller;

        public CpuAgentMetricControllerUnitTests()
        {
            _controller = new CpuAgentController();
        }
        [Fact]
        public void GetMetricsFromAgent_ReturnsOk()
        {
         
            var fromTime = TimeSpan.FromSeconds(0);
            var toTime = TimeSpan.FromSeconds(100);
            var result = _controller.GetMetricsCpu(fromTime, toTime);
            _ = Assert.IsAssignableFrom<IActionResult>(result);

        }
    }

    public class DotNetAgentControllerUnitTests
    {
        private DotNetAgentController _controller;

        public DotNetAgentControllerUnitTests()
        {
            _controller = new DotNetAgentController();
        }
        [Fact]
        public void GetMetricsFromAgent_ReturnsOk()
        {
         
            var fromTime = TimeSpan.FromSeconds(0);
            var toTime = TimeSpan.FromSeconds(100);
            var result = _controller.GetMetricsDotNet(fromTime, toTime);
            _ = Assert.IsAssignableFrom<IActionResult>(result);

        }
    }

    public class HddAgentControllerUnitTests
    {
        private HddAgentController _controller;

        public HddAgentControllerUnitTests()
        {
            _controller = new HddAgentController();
        }
        [Fact]
        public void GetMetricsFromAgent_ReturnsOk()
        {
         
            var fromTime = TimeSpan.FromSeconds(0);
            var toTime = TimeSpan.FromSeconds(100);
            var result = _controller.GetMetricsHdd(fromTime, toTime);
            _ = Assert.IsAssignableFrom<IActionResult>(result);

        }
    }

    public class NetworkAgentControllerUnitTests
    {
        private NetworkAgentController _controller;

        public NetworkAgentControllerUnitTests()
        {
            _controller = new NetworkAgentController();
        }
        [Fact]
        public void GetMetricsFromAgent_ReturnsOk()
        {
         
            var fromTime = TimeSpan.FromSeconds(0);
            var toTime = TimeSpan.FromSeconds(100);
            var result = _controller.GetMetricsNetwork(fromTime, toTime);
            _ = Assert.IsAssignableFrom<IActionResult>(result);

        }

    }

    public class RamAgentControllerUnitTests
    {
        private RamAgentController _controller;

        public RamAgentControllerUnitTests()
        {
            _controller = new RamAgentController();
        }
        [Fact]
        public void GetMetricsFromAgent_ReturnsOk()
        {
         
            var fromTime = TimeSpan.FromSeconds(0);
            var toTime = TimeSpan.FromSeconds(100);
            var result = _controller.GetMetricsRam(fromTime, toTime);
            _ = Assert.IsAssignableFrom<IActionResult>(result);

        }
    }
}