using AnApiOfIceAndFire.Data;
using AnApiOfIceAndFire.Data.Books;
using AnApiOfIceAndFire.Data.Characters;
using AnApiOfIceAndFire.Data.Houses;
using AnApiOfIceAndFire.Database.Seeder;
using Dapper;
using Microsoft.Data.Sqlite;
using Microsoft.Extensions.Options;

namespace AnApiOfIceAndFire.Setup.Tests
{
    [Collection(nameof(SetupDbCollection))]
    public class SetupHelperTests
    {
        private readonly SetupDbFixture fixture;

        public SetupHelperTests(SetupDbFixture fixture)
        {
            this.fixture = fixture;
        }

        [Fact]
        public void SetupDatabase_DatabaseIsPopulated()
        {
            using var connection = new SqliteConnection(fixture.DbPath);
            var nrOfBooks = connection.QueryFirst<int>("SELECT COUNT(Id) FROM Books");
            var nrofCharacters = connection.QueryFirst<int>("SELECT COUNT(Id) FROM Characters");
            var nrOfHouses = connection.QueryFirst<int>("SELECT COUNT(Id) FROM Houses");

            Assert.Equal(DataConstants.BooksMaxId, nrOfBooks);
            Assert.Equal(DataConstants.CharactersMaxId, nrofCharacters);
            Assert.Equal(DataConstants.HousesMaxId, nrOfHouses);
        }

        [Fact]
        public async Task Books_NonExistingBook_ShouldReturnNull()
        {
            var repo = new BookRepository(fixture.ConnectionOptions);

            var book = await repo.GetEntityAsync(DataConstants.BooksMaxId + 1);

            Assert.Null(book);
        }

        [Fact]
        public async Task Books_Existing_CorrectBookIsReturned()
        {
            var repo = new BookRepository(fixture.ConnectionOptions);

            var book = await repo.GetEntityAsync(1);

            Assert.NotNull(book);
            Assert.Equal(1, book.Id);
            Assert.Equal("A Game of Thrones", book.Name);
            Assert.Equal(MediaType.Hardcover, book.MediaType);
            Assert.Equal("George R. R. Martin", book.Authors);
            Assert.Equal("United States", book.Country);
            Assert.Equal("978-0553103540", book.ISBN);
            Assert.Equal(694, book.NumberOfPages);
            Assert.Equal("Bantam Books", book.Publisher);
            Assert.Equal(new DateTime(1996, 8, 1), book.ReleaseDate);
            Assert.Equal(434, book.CharacterIdentifiers.Count);
            Assert.Equal(9, book.PovCharacterIdentifiers.Count);
        }

        [Fact]
        public async Task Books_FilterIsUsed_CorrectBooksReturned()
        {
            var repo = new BookRepository(fixture.ConnectionOptions);

            var books = await repo.GetPaginatedEntitiesAsync(1, 10, new BookFilter()
            {
                Name = "A Clash of Kings",
                FromReleaseDate = new DateTime(1999, 1, 1),
                ToReleaseDate = new DateTime(2000, 1, 1)
            });

            Assert.Equal(1, books.Count);
            Assert.Equal(1, books.PageCount);
            Assert.Equal(2, books[0].Id);
            Assert.Equal(778, books[0].CharacterIdentifiers.Count);
            Assert.Equal(10, books[0].PovCharacterIdentifiers.Count);
        }

        [Fact]
        public async Task Characters_NonExistingCharacter_ShouldReturnNull()
        {
            var repo = new BookRepository(fixture.ConnectionOptions);

            var character = await repo.GetEntityAsync(DataConstants.CharactersMaxId + 1);

            Assert.Null(character);
        }

        [Fact]
        public async Task Characters_Existing_CorrectCharacterIsReturned()
        {
            var repo = new CharacterRepository(fixture.ConnectionOptions);

            var character = await repo.GetEntityAsync(148);

            Assert.NotNull(character);
            Assert.Equal(148, character.Id);
            Assert.Equal("Arya Stark", character.Name);
            Assert.Equal("Northmen", character.Culture);
            Assert.True(!string.IsNullOrEmpty(character.Born));
            Assert.True(string.IsNullOrEmpty(character.Died));
            Assert.Equal(Gender.Female, character.Gender);
            Assert.NotEmpty(character.Aliases);
            Assert.NotEmpty(character.Titles);
            Assert.NotEmpty(character.TvSeries);
            Assert.NotEmpty(character.PlayedBy);
        }

        [Fact]
        public async Task Characters_FilterIsUsed_CorrectCharactersReturned()
        {
            var repo = new CharacterRepository(fixture.ConnectionOptions);

            var characters = await repo.GetPaginatedEntitiesAsync(1, 10, new CharacterFilter()
            {
                Name = "Arya Stark",
                Culture = "Northmen"
            });

            Assert.Equal(1, characters.Count);
            Assert.Equal(1, characters.PageCount);
            Assert.Equal(148, characters[0].Id);
        }

        [Fact]
        public async Task Houses_NonExistingHouse_ShouldReturnNull()
        {
            var repo = new BookRepository(fixture.ConnectionOptions);

            var house = await repo.GetEntityAsync(DataConstants.HousesMaxId + 1);

            Assert.Null(house);
        }

        [Fact]
        public async Task Houses_Existing_CorrectHouseIsReturned()
        {
            var repo = new HouseRepository(fixture.ConnectionOptions);

            var house = await repo.GetEntityAsync(362);

            Assert.NotNull(house);
            Assert.Equal(362, house.Id);
            Assert.Equal("House Stark of Winterfell", house.Name);
            Assert.NotEmpty(house.Seats);
            Assert.Equal("The North", house.Region);
            Assert.False(string.IsNullOrEmpty(house.CoatOfArms));
            Assert.Equal("Winter is Coming", house.Words);
            Assert.NotEmpty(house.Titles);
            Assert.Equal(209, house.FounderId);
            Assert.False(string.IsNullOrEmpty(house.Founded));
            Assert.NotEmpty(house.CadetBranchIdentifiers);
            Assert.True(house.SwornMemberIdentifiers.Count > 0);
        }

        [Fact]
        public async Task Houses_FilterIsUsed_CorrectHousesReturned()
        {
            var repo = new HouseRepository(fixture.ConnectionOptions);
            var filter = new HouseFilter()
            {
                Name = "House Stark of Winterfell",
                HasWords = true,
                HasDiedOut = false,
                HasAncestralWeapons = true,
                HasSeats = true,
                HasTitles = true,
                Words = "Winter is Coming",
                Region = "The North"
            };

            var houses = await repo.GetPaginatedEntitiesAsync(1, 10, filter);

            Assert.Equal(1, houses.Count);
            Assert.Equal(362, houses[0].Id);
            Assert.True(houses[0].CadetBranchIdentifiers.Count > 0);
            Assert.True(houses[0].SwornMemberIdentifiers.Count > 0);
        }
    }

    [CollectionDefinition(nameof(SetupDbCollection))]
    public class SetupDbCollection : ICollectionFixture<SetupDbFixture>
    {
    }

    public class SetupDbFixture : IDisposable
    {
        public string DbPath { get; }
        public IOptions<ConnectionOptions> ConnectionOptions { get; }

        public SetupDbFixture()
        {
            var currentFolder = Directory.GetCurrentDirectory();
            var path = Path.Join(currentFolder, $"{Guid.NewGuid()}.db");
            var dbPath = SetupHelper.SetupDatabase(path);
            DbPath = $"Data Source={dbPath}";
            ConnectionOptions = new OptionsWrapper<ConnectionOptions>(new ConnectionOptions()
            {
                AnApiOfIceAndFireDatabase = DbPath
            });
        }

        public void Dispose()
        {

        }
    }
}