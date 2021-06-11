using Microsoft.AspNetCore.Mvc;

namespace MetricsManager.Controllers
{
    [Route("api/ram/available")]
    [ApiController]
    public class RamMetricsController : Controller
    {
        // GET
        public IActionResult Index()
        {
            return View();
        }
    }
}