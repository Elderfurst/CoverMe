using Microsoft.AspNetCore.Mvc;
using System;

namespace CoverMe.Controllers
{
    public class HomeController : Controller
    {
        public HomeController() {}

        public IActionResult Index()
        {
            var timeZones = TimeZoneInfo.GetSystemTimeZones();

            return View(timeZones);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View();
        }
    }
}
