using Microsoft.AspNetCore.Mvc;

namespace MetricsManager.Controllers
{
    [Route("api/metrics/hdd/left")]
    [ApiController]
    public class HddMetricsController : Controller
    {
        // GET
        public IActionResult Index()
        {
            return View();
        }
    }
}