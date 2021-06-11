using Microsoft.AspNetCore.Mvc;

namespace MetricsAgent.Controllers
{
    public class NetworkAgentController : Controller
    {
        // GET
        public IActionResult Index()
        {
            return View();
        }
    }
}