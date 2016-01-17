using System;
using System.Web.Mvc;

namespace AnApiOfIceAndFire.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Title = "Home Page";

            return View();
        }

        [HttpPost]
        public ActionResult Charge(FormCollection fc)
        {
            return Redirect("/");
        }
    }
}
