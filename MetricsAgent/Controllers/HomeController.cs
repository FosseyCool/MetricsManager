using System.Collections.Generic;
using System.Linq;
using Core.Intefaces;
using Microsoft.AspNetCore.Mvc;

namespace MetricsAgent.Controllers
{
    public class HomeController : Controller
    {
        private readonly INotifierMediatorService _notifierMediatorService;

        public HomeController(INotifierMediatorService notifierMediatorService)
        {
            _notifierMediatorService = notifierMediatorService;
        }

        [HttpGet("")]
        public ActionResult<string> NotifyAll()
        {
            _notifierMediatorService.Notify();
            return "Completed";
        }

    }
}