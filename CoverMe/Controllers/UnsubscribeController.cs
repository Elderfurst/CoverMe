using Microsoft.AspNetCore.Mvc;
using System;

namespace CoverMe.Controllers
{
    public class UnsubscribeController : Controller
    {
        public UnsubscribeController() { }

        public IActionResult Index()
        {
            return View();
        }
    }
}
