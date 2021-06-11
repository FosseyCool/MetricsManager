using Microsoft.AspNetCore.Mvc;

namespace MetricsManager.Controllers
{
    [Route("api/network")]
    [ApiController]
    public class NetworkMetricsController : Controller
    {
        // GET
        public IActionResult Index()
        {
            return View();
        }
    }
}