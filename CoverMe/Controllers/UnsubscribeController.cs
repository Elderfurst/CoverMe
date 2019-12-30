using Microsoft.AspNetCore.Mvc;

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
