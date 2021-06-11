using Microsoft.AspNetCore.Mvc;

namespace MetricsAgent.Controllers
{
    public class RamAgentController : Controller
    {
        // GET
        public IActionResult Index()
        {
            return View();
        }
    }
}