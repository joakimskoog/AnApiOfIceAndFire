using Microsoft.AspNetCore.Mvc;

namespace AnApiOfIceAndFire.Controllers
{
    public class HomeController : Controller
    {
        [ResponseCache(Location = ResponseCacheLocation.Any, Duration = 600)]
        public IActionResult Index()
        {
            return View();
        }
    }
}