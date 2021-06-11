using Microsoft.AspNetCore.Mvc;

namespace MetricsAgent.Controllers
{
    public class DotNetAgentController : Controller
    {
        // GET
        public IActionResult Index()
        {
            return View();
        }
    }
}