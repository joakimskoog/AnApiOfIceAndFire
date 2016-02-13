using System.Diagnostics;
using System.Web;
using AnApiOfIceAndFire.Data;
using AnApiOfIceAndFire.Data.Feeder;

[assembly: WebActivatorEx.PostApplicationStartMethod(typeof(AnApiOfIceAndFire.DataFeederActivator), "Start")]

namespace AnApiOfIceAndFire
{
    public class DataFeederActivator
    {
        public static void Start()
        {
            /*
             * Uncomment the rows below if you want to create a local database and fill it with data.
             * Note that this can take up to 2 minutes the first time. After that it only takes a few seconds.
             */
            //var booksPath = HttpContext.Current.Server.MapPath(@"~/bin/Data/books.json");
            //var charactersPath = HttpContext.Current.Server.MapPath(@"~/bin/Data/characters.json");
            //var housesPath = HttpContext.Current.Server.MapPath(@"~/bin/Data/houses.json");

            //Feeder.FeedDatabase(new AnApiOfIceAndFireContext(), booksPath, charactersPath,housesPath, s => Debug.WriteLine(s));
        }
    }
}