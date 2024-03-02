using AnApiOfIceAndFire.Database.Seeder;
using Dapper;
using Microsoft.Data.Sqlite;

namespace AnApiOfIceAndFire.Setup.Tests
{
    public class SetupHelperTests
    {
        [Fact]
        public void SetupDatabase_DatabaseIsPopulated()
        {
            var currentFolder = Directory.GetCurrentDirectory();
            var path = Path.Join(currentFolder, "anapioficeandfire.db");
            var dbPath = SetupHelper.SetupDatabase(path);

            using var connection = new SqliteConnection($"Data Source={dbPath}");
            var nrOfBooks = connection.QueryFirst<int>("SELECT COUNT(Id) FROM Books");
            var nrofCharacters = connection.QueryFirst<int>("SELECT COUNT(Id) FROM Characters");
            var nrOfHouses = connection.QueryFirst<int>("SELECT COUNT(Id) FROM Houses");

            Assert.Equal(DataConstants.BooksMaxId, nrOfBooks);
            Assert.Equal(DataConstants.CharactersMaxId, nrofCharacters);
            Assert.Equal(DataConstants.HousesMaxId, nrOfHouses);
        }
    }
}