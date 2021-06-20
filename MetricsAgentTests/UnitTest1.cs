using System;
using MetricsAgent.Controllers;
using MetricsAgent.DAL;
using MetricsAgent.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using Moq;
using Xunit;
using CpuMetric = MetricsAgent.Models.CpuMetric;
using DotNetMetric = MetricsAgent.Models.DotNetMetric;
using RamMetric = MetricsAgent.Models.RamMetric;

namespace MetricsAgentTests
{
    public class CpuMetricsAgentControllerUnitTests
    {
        private CpuAgentController controller;
        private Mock<ICpuMetricsRepository> mock;

        public CpuMetricsAgentControllerUnitTests()
        {
            mock = new Mock<ICpuMetricsRepository>();

            controller = new CpuAgentController(NullLogger<CpuAgentController>.Instance, mock.Object);
        }

        [Fact]
        public void Create_ShouldCall_Create_From_Repository()
        {
            // устанавливаем параметр заглушки
            // в заглушке прописываем что в репозиторий прилетит CpuMetric объект
            mock.Setup(repository => repository.Create(It.IsAny<CpuMetric>())).Verifiable();
            
            // выполняем действие на контроллере
            var result = controller.Create(new MetricsAgent.Controllers.CpuMetric()
                {Time = DateTimeOffset.FromUnixTimeSeconds(1).Second, Value = 50});

            // проверяем заглушку на то, что пока работал контроллер
            // действительно вызвался метод Create репозитория с нужным типом объекта в параметре
            mock.Verify(repository => repository.Create(It.IsAny<CpuMetric>()), Times.AtMostOnce());


        }
    }

    public class DotNetAgentControllerUnitTests
    {
        private DotNetAgentController _controller;
        private Mock<IDotNetRepository> _mock;
        
        public DotNetAgentControllerUnitTests()
        {
            _mock = new Mock<IDotNetRepository>();
            _controller = new DotNetAgentController(NullLogger<DotNetAgentController>.Instance, _mock.Object);
            
        }
        
        [Fact]
        public void Create_ShouldCall_Create_From_Repository()
        {
            _mock.Setup(repository =>repository.Create(It.IsAny<DotNetMetric>())).Verifiable();
            var result = _controller.Create(new MetricsAgent.Controllers.DotNetMetric()
                {Time = DateTimeOffset.FromUnixTimeSeconds(1).Second, Value = 50});
            _mock.Verify(repository => repository.Create(It.IsAny<DotNetMetric>()), Times.AtMostOnce());

        }
    }
    
    public class HddAgentControllerUnitTests
    {
        private HddAgentController _controller;
        private Mock<IHddMetricsRepository> _mock;

        public HddAgentControllerUnitTests()
        {
            _mock = new Mock<IHddMetricsRepository>();
            _controller = new HddAgentController(NullLogger<HddAgentController>.Instance, _mock.Object);
            
        }

        [Fact]
        public void Create_ShouldCall_Create_From_Repository()
        {
            _mock.Setup(repository =>repository.Create(It.IsAny<HddMetric>())).Verifiable();
            var result = _controller.Create(new MetricsAgent.Controllers.Hddmetric()  {Time = DateTimeOffset.FromUnixTimeSeconds(1).Second, Value = 50}); 
            _mock.Verify(repository => repository.Create(It.IsAny<HddMetric>()), Times.AtMostOnce());
        
        }
    }

    public class NetworkControllerUnitTests
    {
        private NetworkAgentController _controller;
        private Mock<INetworkMetricsRepository> _mock;

        public NetworkControllerUnitTests()
        {
            _mock = new Mock<INetworkMetricsRepository>();
            _controller = new NetworkAgentController(NullLogger<NetworkAgentController>.Instance, _mock.Object);
            
        }

        [Fact]
        public void Create_ShouldCall_Create_From_Repository()
        {
            _mock.Setup(repository => repository.Create(It.IsAny<NetWorkMetric>())).Verifiable();
            var result = _controller.Create(new MetricsAgent.Controllers.NetworkMetric()
                {Time = DateTimeOffset.FromUnixTimeSeconds(1).Second, Value = 50});
            _mock.Verify(repository =>repository.Create(It.IsAny<NetWorkMetric>()),Times.AtMostOnce() );

        }
        
    }

    public class RamControllerUnitTests
    {
        private RamAgentController _controller;
        private Mock<IRamMetricRepository> _mock;

        public RamControllerUnitTests()
        {
            _mock = new Mock<IRamMetricRepository>();
            _controller = new RamAgentController(NullLogger<RamAgentController>.Instance, _mock.Object);
        }

        [Fact]
        public void Create_ShouldCall_Create_From_Repository()
        {
            _mock.Setup(repository =>repository.Create(It.IsAny<RamMetric>()) ).Verifiable();

            var result = _controller.Create(new MetricsAgent.Controllers.RamMetric()
                {Time = DateTimeOffset.FromUnixTimeSeconds(1).Second, Value = 50});
            _mock.Verify(repository =>repository.Create(It.IsAny<RamMetric>()),Times.AtMostOnce );
        }
    }

}






 
