using Microsoft.AspNetCore.Mvc;

namespace AnApiOfIceAndFire.Controllers
{
    public class DocumentationController : Controller
    {
        [ResponseCache(Location = ResponseCacheLocation.Any, Duration = 600)]
        public IActionResult Index()
        {
            return View();
        }
    }
}