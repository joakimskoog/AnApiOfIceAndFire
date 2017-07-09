using System;
using System.Diagnostics;
using System.Threading.Tasks;
using AnApiOfIceAndFire.Data.Books;
using AnApiOfIceAndFire.DataFeeder;
using Microsoft.Extensions.Options;
using Xunit;

namespace AnApiOfIceAndFire.Data.Tests.IntegrationTests
{
    public class BookRepositoryTests : IClassFixture<DbFixture>
    {
        [Fact]
        public async Task GetEntityAsync_NoBookWithGivenId_NullIsReturned()
        {
            var options = new OptionsWrapper<ConnectionOptions>(new ConnectionOptions()
            {
                ConnectionString = DbFixture.ConnectionString
            });
            var repo = new BookRepository(options);

            var book = await repo.GetEntityAsync(1337);

            Assert.Null(book);
        }
    }

    public class DbFixture : IDisposable
    {
        
        public const string MasterConnection = @"Server=(local)\SQL2016;Database=master;User ID = sa; Password=Password12!";
        public const string ConnectionString = @"Server=(local)\SQL2016;Database=anapioficeandfire;User ID = sa; Password=Password12!";


        //public const string MasterConnection = @"Server=localhost\SQLEXPRESS;Database=master;Trusted_Connection=True;MultipleActiveResultSets=true;";
        //public const string ConnectionString = @"Server=localhost\SQLEXPRESS;Database=anapioficeandfire;Trusted_Connection=True;MultipleActiveResultSets=true;";


        public DbFixture()
        {
            try
            {
                DatabaseFeeder.CreateDatabase(MasterConnection);
                DatabaseFeeder.CreateTables(ConnectionString);
                
            }
            catch (Exception)
            {
                DatabaseFeeder.CleanDatabase(MasterConnection);
            }
        }

        public void Dispose()
        {
            DatabaseFeeder.CleanDatabase(MasterConnection);
            Debug.WriteLine("dead");

        }
    }
}