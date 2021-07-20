using MetricsManager.DAL.Interfaces;
using MetricsManager.Models;
using Microsoft.AspNetCore.Mvc;

namespace MetricsManager.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AgentsController : ControllerBase
    {
        private IAgentRepository _repository;


        public AgentsController(IAgentRepository repository)
        {
            _repository = repository;
        }

        [HttpPost("register")]
        public IActionResult RegisterAgent([FromBody] AgentInfo agentInfo)
        {
            _repository.RegisterAgent(agentInfo);
            return Ok();
        }

        [HttpPut("enable/{agentId}")]
        public IActionResult EnableAgentById([FromRoute] int agentId)
        {
            _repository.EnableById(agentId);
            return Ok();
        }

        [HttpPut("disable/{agentId}")]
        public IActionResult DisableAgentById([FromRoute] int agentId)
        {
            _repository.DisableById(agentId);
            return Ok();
        }

        [HttpGet("getList")]
        public IActionResult GetRegisteredList()
        {
            return Ok(_repository.GetRegisteredList());
        }
    }
}