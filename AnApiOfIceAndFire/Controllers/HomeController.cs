using System;
using System.Diagnostics;
using System.Web.Configuration;
using System.Web.Http;
using System.Web.Mvc;
using AnApiOfIceAndFire.Infrastructure;
using Stripe;

namespace AnApiOfIceAndFire.Controllers
{
    public class HomeController : Controller
    {
        private readonly IApiSettings _settings;

        public HomeController(IApiSettings settings)
        {
            if (settings == null) throw new ArgumentNullException(nameof(settings));
            _settings = settings;
        }

        public ActionResult Index()
        {
            ViewBag.Title = "Home Page";
            ViewBag.StripeDonationCurrency = _settings.StripeDonationCurrency;
            ViewBag.StripePublicKey = _settings.StripePublicKey;
            ViewBag.StripeDonationAmountInCents = _settings.StripeDonationAmountInCents;

            return View();
        }

        [System.Web.Mvc.HttpPost]
        public ActionResult Charge([FromBody]string stripeEmail, string stripeToken)
        {
            try
            {
                var charge = new StripeChargeCreateOptions()
                {
                    Amount = _settings.StripeDonationAmountInCents,
                    Currency = _settings.StripeDonationCurrency,
                    Description = "Donation for An API of Ice And Fire",
                    ReceiptEmail = stripeEmail,
                    Source = new StripeSourceOptions
                    {
                        TokenId = stripeToken
                    }
                };

                var chargeService = new StripeChargeService(_settings.StripePrivateKey);
                chargeService.Create(charge);
            }
            catch (StripeException se)
            {
                //Log error in a better way later on
                Debug.WriteLine(se.StripeError);
            }

            return Redirect("/");
        }
    }
}
