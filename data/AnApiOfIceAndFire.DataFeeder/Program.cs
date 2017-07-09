using System;

namespace AnApiOfIceAndFire.DataFeeder
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length == 0)
            {
                throw new ArgumentException("Please specify the folder where all scripts and data resides!");
            }

            var masterConnectionString = args[0];
            var dbConnectionString = args[1];

            DatabaseFeeder.RunAllScripts(masterConnectionString, dbConnectionString, @"..\..\data\");
            Console.ReadLine();
        }
    }
}