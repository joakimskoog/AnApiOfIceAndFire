namespace AnApiOfIceAndFire.Infrastructure
{
    public interface IApiSettings
    {
        string StripePublicKey { get; }
        string StripePrivateKey { get; }
        string StripeDonationCurrency { get; }
        int StripeDonationAmountInCents { get; }
    }
}