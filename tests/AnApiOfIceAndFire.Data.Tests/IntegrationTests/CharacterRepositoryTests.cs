using System.Threading.Tasks;
using AnApiOfIceAndFire.Data.Characters;
using Microsoft.Extensions.Options;
using Xunit;

namespace AnApiOfIceAndFire.Data.Tests.IntegrationTests
{
    [Collection("DbCollection")]
    public class CharacterRepositoryTests
    {
        [Fact]
        public async Task GetEntityAsync_NoCharacterWithGivenId_NullIsReturned()
        {
            var options = new OptionsWrapper<ConnectionOptions>(new ConnectionOptions()
            {
                ConnectionString = DbFixture.ConnectionString
            });
            var repo = new CharacterRepository(options);

            var character = await repo.GetEntityAsync(9001);

            Assert.Null(character);
        }

        [Fact]
        public async Task GetEntityAsync_CharacterWithGivenId_ThatCharacterIsReturned()
        {
            var options = new OptionsWrapper<ConnectionOptions>(new ConnectionOptions()
            {
                ConnectionString = DbFixture.ConnectionString
            });
            var repo = new CharacterRepository(options);

            var character = await repo.GetEntityAsync(148);

            Assert.NotNull(character);
            Assert.Equal(148, character.Id);
            Assert.Equal("Arya Stark", character.Name);
            Assert.Equal("Northmen", character.Culture);
            Assert.True(!string.IsNullOrEmpty(character.Born));
            Assert.True(string.IsNullOrEmpty(character.Died));
            Assert.True(character.IsFemale);
            Assert.NotEmpty(character.ParseAliases());
            Assert.NotEmpty(character.ParseTitles());
            Assert.NotEmpty(character.ParseTvSeries());
            Assert.NotEmpty(character.ParsePlayedBy());
        }

        [Fact]
        public async Task GetPaginatedEntitiesAsync_FilterWithNameAndCulture_CorrectCharacterIsReturned()
        {
            var options = new OptionsWrapper<ConnectionOptions>(new ConnectionOptions()
            {
                ConnectionString = DbFixture.ConnectionString
            });
            var repo = new CharacterRepository(options);

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
        public async Task GetPaginatedEntitiesAsync_FilterWithIsAliveAndFemale_CorrectCharactersAreReturned()
        {
            var options = new OptionsWrapper<ConnectionOptions>(new ConnectionOptions()
            {
                ConnectionString = DbFixture.ConnectionString
            });
            var repo = new CharacterRepository(options);

            var characters = await repo.GetPaginatedEntitiesAsync(1, 10, new CharacterFilter()
            {
                Gender = Gender.Female,
                IsAlive = true
            });

            Assert.True(characters.Count > 0);
            foreach (var character in characters)
            {
                Assert.Empty(character.Died);
                Assert.True(character.IsFemale);
            }
        }

        [Fact]
        public async Task GetPaginatedEntitiesAsync_FilterWithIsDeadAndMale_CorrectCharactersAreReturned()
        {
            var options = new OptionsWrapper<ConnectionOptions>(new ConnectionOptions()
            {
                ConnectionString = DbFixture.ConnectionString
            });
            var repo = new CharacterRepository(options);

            var characters = await repo.GetPaginatedEntitiesAsync(1, 10, new CharacterFilter()
            {
                Gender = Gender.Male,
                IsAlive = false
            });

            Assert.True(characters.Count > 0);
            foreach (var character in characters)
            {
                Assert.NotEmpty(character.Died);
                Assert.False(character.IsFemale);
            }
        }
    }
}