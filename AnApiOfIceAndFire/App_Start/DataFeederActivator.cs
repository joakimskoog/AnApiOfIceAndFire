[assembly: WebActivatorEx.PostApplicationStartMethod(typeof(AnApiOfIceAndFire.DataFeederActivator), "Start")]

namespace AnApiOfIceAndFire
{
    public class DataFeederActivator
    {
        public static void Start()
        {
            /*
             * Uncomment the row below if you want to create a local database and fill it with data.
             * Note that this can take up to 2 minutes the first time. After that it only takes a few seconds.
             */
            //Feeder.FeedDatabase(new AnApiOfIceAndFireContext(), "urlToBooksHere", "urlToCharactersHere", "urlToHousesHere", s => Debug.WriteLine(s));
        }
    }
}