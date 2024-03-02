//using System;
//using System.Threading.Tasks;
//using AnApiOfIceAndFire.Data.Books;
//using Microsoft.Extensions.Options;
//using Xunit;

//namespace AnApiOfIceAndFire.Data.Tests.IntegrationTests
//{
//    [Collection("DbCollection")]
//    public class BookRepositoryTests
//    {
//        [Fact]
//        public async Task GetEntityAsync_NoBookWithGivenId_NullIsReturned()
//        {
//            var options = new OptionsWrapper<ConnectionOptions>(new ConnectionOptions()
//            {
//                ConnectionString = DbFixture.ConnectionString
//            });
//            var repo = new BookRepository(options);

//            var book = await repo.GetEntityAsync(9001);

//            Assert.Null(book);
//        }

//        [Fact]
//        public async Task GetEntityAsync_BookWithGivenId_ThatBookIsReturned()
//        {
//            var options = new OptionsWrapper<ConnectionOptions>(new ConnectionOptions()
//            {
//                ConnectionString = DbFixture.ConnectionString
//            });
//            var repo = new BookRepository(options);

//            var book = await repo.GetEntityAsync(1);

//            Assert.NotNull(book);
//            Assert.Equal(1, book.Id);
//            Assert.Equal("A Game of Thrones", book.Name);
//            Assert.Equal(MediaType.Hardcover, book.MediaType);
//            Assert.Equal("George R. R. Martin", book.Authors);
//            Assert.Equal("United States", book.Country);
//            Assert.Equal("978-0553103540", book.ISBN);
//            Assert.Equal(694, book.NumberOfPages);
//            Assert.Equal("Bantam Books", book.Publisher);
//            Assert.Equal(new DateTime(1996,8,1), book.ReleaseDate);
//            Assert.Equal(434, book.CharacterIdentifiers.Count);
//            Assert.Equal(9, book.PovCharacterIdentifiers.Count);
//        }

//        [Fact]
//        public async Task GetPaginatedEntitiesAsync_FilterApplied_CorrectBookIsReturned()
//        {
//            var options = new OptionsWrapper<ConnectionOptions>(new ConnectionOptions()
//            {
//                ConnectionString = DbFixture.ConnectionString
//            });
//            var repo = new BookRepository(options);

//            var books = await repo.GetPaginatedEntitiesAsync(1, 10, new BookFilter()
//            {
//                Name = "A Clash of Kings",
//                FromReleaseDate = new DateTime(1999, 1, 1),
//                ToReleaseDate = new DateTime(2000, 1, 1)
//            });

//            Assert.Equal(1, books.Count);
//            Assert.Equal(1, books.PageCount);
//            Assert.Equal(2, books[0].Id);
//            Assert.Equal(778, books[0].CharacterIdentifiers.Count);
//            Assert.Equal(10, books[0].PovCharacterIdentifiers.Count);

//        }
//    }

//    //todo: Fix this so we dont have to rely on compile flags when testing in our CI tool. It's kind of hard to maintain, find out if AppVeyor has a better way.
//}