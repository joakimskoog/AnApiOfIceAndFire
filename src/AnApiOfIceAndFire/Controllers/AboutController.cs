using Microsoft.AspNetCore.Mvc;

namespace AnApiOfIceAndFire.Controllers
{
    public class AboutController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}