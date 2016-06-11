using System.Web.Http;
using WebActivatorEx;
using AnApiOfIceAndFire;
using Headmaster.Configuration;

[assembly: PreApplicationStartMethod(typeof(HeaderVersioningConfig), "Register")]

namespace AnApiOfIceAndFire
{
    public class HeaderVersioningConfig
    {
        public const string AllowedAcceptHeaderMediaType = "application/vnd.anapioficeandfire+json";
        public const string AllowedAcceptHeaderMediaTypeParameter = "version";


        public static void Register()
        {
            GlobalConfiguration.Configuration.EnableHeaderVersioning(new HeaderVersioningOptions(AllowedAcceptHeaderMediaType,
                AllowedAcceptHeaderMediaTypeParameter, DefaultVersionResolving.UseLatestIfEmpty,
                requestVersion => $"v{requestVersion}"
                ));
        }
    }
}
