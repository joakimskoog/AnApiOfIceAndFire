using Microsoft.AspNetCore.Mvc;

namespace AnApiOfIceAndFire.Controllers
{
    public class SponsorController : Controller
    {
        [ResponseCache(Location = ResponseCacheLocation.Any, Duration = 600)]
        public IActionResult Index()
        {
            return View();
        }
    }
}