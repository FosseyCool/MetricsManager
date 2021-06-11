using Microsoft.AspNetCore.Mvc;

namespace MetricsAgent.Controllers
{
    public class HddAgentController : Controller
    {
        // GET
        public IActionResult Index()
        {
            return View();
        }
    }
}