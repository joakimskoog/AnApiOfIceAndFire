using System.Web.Http;
using WebActivatorEx;
using AnApiOfIceAndFire;
using Headmaster.Configuration;

[assembly: PreApplicationStartMethod(typeof(HeaderVersioningConfig), "Register")]

namespace AnApiOfIceAndFire
{
    public class HeaderVersioningConfig
    {
		public static void Register() 
		{
			GlobalConfiguration.Configuration.EnableHeaderVersioning(new HeaderVersioningOptions("application/vnd.anapioficeandfire+json", "version", DefaultVersionResolving.UseLatestIfEmpty, "v"));
		}
    }
}
