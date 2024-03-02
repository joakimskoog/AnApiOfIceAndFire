//using System;
//using AnApiOfIceAndFire.DataFeeder;
//using Xunit;

//namespace AnApiOfIceAndFire.Data.Tests.IntegrationTests
//{
//    [CollectionDefinition("DbCollection")]
//    public class DbCollection : ICollectionFixture<DbFixture>
//    {

//    }

//    public class DbFixture : IDisposable
//    {
//#if RELEASE
//                public const string MasterConnection = @"Server =(local)\SQL2014;Database=master;User ID = sa; Password=Password12!;MultipleActiveResultSets=true;";
//                public const string ConnectionString = @"Server=(local)\SQL2014;Database=master;User ID = sa; Password=Password12!;MultipleActiveResultSets=true;";
//#endif

//#if DEBUG
//        public const string MasterConnection = @"Server=localhost\SQLEXPRESS;Database=master;Trusted_Connection=True;MultipleActiveResultSets=true;";
//        public const string ConnectionString = @"Server=localhost\SQLEXPRESS;Database=anapioficeandfire;Trusted_Connection=True;MultipleActiveResultSets=true;";
//#endif

//        public DbFixture()
//        {
//            DatabaseFeeder.RunAllScripts(MasterConnection, ConnectionString, @"..\..\..\..\..\data\");
//        }

//        public void Dispose()
//        {
//#if DEBUG
//            DatabaseFeeder.CleanDatabase(MasterConnection);
//#endif
//        }
//    }
//}