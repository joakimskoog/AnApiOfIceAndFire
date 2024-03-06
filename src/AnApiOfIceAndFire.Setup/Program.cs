using AnApiOfIceAndFire.Database.Seeder;
using System.Diagnostics;

Console.WriteLine("Building database, please wait...");
var sw = Stopwatch.StartNew();

var dbPath = SetupHelper.SetupDatabase();

sw.Stop();
Console.WriteLine($"Database built in {sw.ElapsedMilliseconds} ms");
Console.WriteLine($"Location: {dbPath}");