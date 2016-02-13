##Contributing

1. Create an [issue](https://github.com/joakimskoog/AnApiOfIceAndFire/issues) or find an [issue](https://github.com/joakimskoog/AnApiOfIceAndFire/issues) that you would like to fix.
2. Fork this repository
3. Implement the new functionality/bug fix.
4. Write tests for your code
5. Submit a pull request that is up to date with the master branch
6. Watch us accept it and deploy it to production

##Worth noting
* All contributions are welcome, it doesn't matter if it's a big feature or a small data fix.
* The data files are located over at AnApiOfIceAndFire.Data.Feeder/Data/

##Development
It's important to have data to test your code with. That's why you need to create the local database and seed it with data. This can be done by uncommenting a line in AnApiOfIceAndFire.DataFeederActivator.cs as seen below:

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
