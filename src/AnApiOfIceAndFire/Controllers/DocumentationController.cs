using Microsoft.AspNetCore.Mvc;

namespace AnApiOfIceAndFire.Controllers
{
    public class DocumentationController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}