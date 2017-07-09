using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using AnApiOfIceAndFire.Data.Books;
using AnApiOfIceAndFire.Data.Characters;
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

        [Fact]
        public async Task GetEntityAsync_BookWithGivenId_ThatBookIsReturned()
        {

            var options = new OptionsWrapper<ConnectionOptions>(new ConnectionOptions()
            {
                ConnectionString = DbFixture.ConnectionString
            });
            var repo = new BookRepository(options);
            var characterRepo = new CharacterRepository(options);
            var testBook = new BookEntity()
            {
                Id = 1,
                Name = "TestBook",
                MediaType = MediaType.GraphicNovel,
                Authors = "Testy McTest",
                Country = "United Kingdom Of Testing",
                ISBN = "978-1233637",
                NumberOfPages = 1337,
                Publisher = "Some Random Publisher",
                ReleaseDate = new DateTime(2030, 1, 10),

            };
            await repo.InsertEntitiesAsync(new List<BookEntity>() { testBook });
            await characterRepo.InsertEntitiesAsync(new List<CharacterEntity>()
            {
                new CharacterEntity()
                {
                    Id = 1,
                    Name = "Boaty McBoatface",
                    Born = "",
                    Died = "",
                    IsFemale = false,
                    BookIdentifiers = new List<int>() {1}
                },
                new CharacterEntity()
                {
                    Id = 2,
                    Name = "Boaty McBoatface Jr",
                    Born = "",
                    Died = "",
                    IsFemale = false,
                    BookIdentifiers = new List<int>() {1}
                },
                new CharacterEntity()
                {
                    Id = 3,
                    Name = "Ms. McBoatface",
                    Born = "",
                    Died = "",
                    IsFemale = true,
                    PovBookIdentifiers = new List<int>(){1}
                }
            });

            var book = await repo.GetEntityAsync(1);

            Assert.NotNull(book);
            Assert.Equal(testBook.Id, book.Id);
            Assert.Equal(testBook.Name, book.Name);
            Assert.Equal(testBook.MediaType, book.MediaType);
            Assert.Equal(testBook.Authors, book.Authors);
            Assert.Equal(testBook.Country, book.Country);
            Assert.Equal(testBook.ISBN, book.ISBN);
            Assert.Equal(testBook.NumberOfPages, book.NumberOfPages);
            Assert.Equal(testBook.Publisher, book.Publisher);
            Assert.Equal(testBook.ReleaseDate, book.ReleaseDate);
            Assert.Equal(2, book.CharacterIdentifiers.Count);
            Assert.Equal(1, book.PovCharacterIdentifiers.Count);
        }

        [Fact]
        public async Task GetPaginatedEntitiesAsync_FilterApplied_CorrectBookIsReturned()
        {
            var options = new OptionsWrapper<ConnectionOptions>(new ConnectionOptions()
            {
                ConnectionString = DbFixture.ConnectionString
            });
            var repo = new BookRepository(options);
            var characterRepo = new CharacterRepository(options);
            await repo.InsertEntitiesAsync(new List<BookEntity>()
            {
                new BookEntity()
                {
                    Id = 10,
                    Name = "PagingBook",
                    Authors = "Author",
                    ReleaseDate = new DateTime(2016, 10, 8),
                    Country = "Sweden",
                    MediaType = MediaType.Hardback,
                    Publisher = "Publisher",
                    ISBN = "978-252626226",
                    NumberOfPages = 100,
                }
            });
            await characterRepo.InsertEntitiesAsync(new List<CharacterEntity>()
            {
                new CharacterEntity()
                {
                    Id = 99,
                    Name = "Test Testsson",
                    Born = "",
                    Died = "",
                    IsFemale = false,
                    BookIdentifiers = new List<int>() {10},
                    PovBookIdentifiers = new List<int>(){10}
                }
            });

            var books = await repo.GetPaginatedEntitiesAsync(1, 10, new BookFilter()
            {
                Name = "PagingBook",
                FromReleaseDate = new DateTime(2016, 1, 1),
                ToReleaseDate = new DateTime(2017, 1, 1)
            });

            Assert.Equal(1, books.Count);
            Assert.Equal(1, books.PageCount);
            Assert.Equal(10, books[0].Id);
            Assert.Equal(1, books[0].CharacterIdentifiers.Count);
            Assert.Equal(1, books[0].PovCharacterIdentifiers.Count);

        }
    }

    public class DbFixture : IDisposable
    {
#if RELEASE
        public const string MasterConnection = @"Server =(local)\SQL2014;Database=master;User ID = sa; Password=Password12!";
        public const string ConnectionString = @"Server=(local)\SQL2014;Database=master;User ID = sa; Password=Password12!";
#endif

#if DEBUG
        public const string MasterConnection = @"Server=localhost\SQLEXPRESS;Database=master;Trusted_Connection=True;MultipleActiveResultSets=true;";
        public const string ConnectionString = @"Server=localhost\SQLEXPRESS;Database=anapioficeandfire;Trusted_Connection=True;MultipleActiveResultSets=true;";
#endif

        public DbFixture()
        {
            try
            {
#if DEBUG
                DatabaseFeeder.CreateDatabase(MasterConnection);
#endif
                DatabaseFeeder.CreateTables(MasterConnection);

            }
            catch (Exception)
            {
#if DEBUG
                DatabaseFeeder.CleanDatabase(MasterConnection);
#endif
            }
        }

        public void Dispose()
        {
            DatabaseFeeder.CleanDatabase(MasterConnection);
            Debug.WriteLine("dead");

        }
    }
}