using System;
using System.Web.Configuration;

namespace AnApiOfIceAndFire.Infrastructure
{
    public class WebConfigSettings : IApiSettings
    {
        public string StripePublicKey
        {
            get
            {
                var value = WebConfigurationManager.AppSettings["StripePublicKey"];
                return value ?? string.Empty;
            }
        }

        public string StripePrivateKey
        {
            get
            {
                var value = WebConfigurationManager.AppSettings["StripePrivateKey"];
                return value ?? string.Empty;
            }
        }

        public string StripeDonationCurrency
        {
            get
            {
                var value = WebConfigurationManager.AppSettings["StripeDonationCurrency"];
                return value ?? string.Empty;
            }
        }

        public int StripeDonationAmountInCents
        {
            get
            {
                var value = WebConfigurationManager.AppSettings["StripeDonationAmountInCents"];
                int amountInCents;
                if (Int32.TryParse(value, out amountInCents))
                {
                    return amountInCents;
                }

                throw new Exception("Could not retrieve donation amount");
            }
        }
    }
}