//using System.Threading.Tasks;
//using AnApiOfIceAndFire.Data.Houses;
//using Microsoft.Extensions.Options;
//using Xunit;

//namespace AnApiOfIceAndFire.Data.Tests.IntegrationTests
//{
//    [Collection("DbCollection")]
//    public class HouseRepositoryTests
//    {
//        [Fact]
//        public async Task GetEntityAsync_NoHouseWithGivenId_NullIsReturned()
//        {
//            var options = new OptionsWrapper<ConnectionOptions>(new ConnectionOptions()
//            {
//                ConnectionString = DbFixture.ConnectionString
//            });
//            var repo = new HouseRepository(options);

//            var house = await repo.GetEntityAsync(9001);

//            Assert.Null(house);
//        }

//        [Fact]
//        public async Task GetEntityAsync_HouseWithMatchingId_CorrectHouseIsReturned()
//        {
//            var options = new OptionsWrapper<ConnectionOptions>(new ConnectionOptions()
//            {
//                ConnectionString = DbFixture.ConnectionString
//            });
//            var repo = new HouseRepository(options);

//            var house = await repo.GetEntityAsync(362);

//            Assert.NotNull(house);
//            Assert.Equal(362, house.Id);
//            Assert.Equal("House Stark of Winterfell", house.Name);
//            Assert.NotEmpty(house.ParseSeats());
//            Assert.Equal("The North", house.Region);
//            Assert.False(string.IsNullOrEmpty(house.CoatOfArms));
//            Assert.Equal("Winter is Coming", house.Words);
//            Assert.NotEmpty(house.ParseTitles());
//            Assert.Equal(209, house.FounderId);
//            Assert.False(string.IsNullOrEmpty(house.Founded));
//            Assert.NotEmpty(house.CadetBranchIdentifiers);
//            Assert.True(house.SwornMemberIdentifiers.Count > 0);
//        }

//        [Fact]
//        public async Task GetPaginatedEntitiesAsync_Filter_CorrectHouseIsReturned()
//        {
//            var options = new OptionsWrapper<ConnectionOptions>(new ConnectionOptions()
//            {
//                ConnectionString = DbFixture.ConnectionString
//            });
//            var repo = new HouseRepository(options);
//            var filter = new HouseFilter()
//            {
//                Name = "House Stark of Winterfell",
//                HasWords = true,
//                HasDiedOut = false,
//                HasAncestralWeapons = true,
//                HasSeats = true,
//                HasTitles = true,
//                Words = "Winter is Coming",
//                Region = "The North"
//            };

//            var houses = await repo.GetPaginatedEntitiesAsync(1, 10, filter);

//            Assert.Equal(1, houses.Count);
//            Assert.Equal(362, houses[0].Id);
//            Assert.True(houses[0].CadetBranchIdentifiers.Count > 0);
//            Assert.True(houses[0].SwornMemberIdentifiers.Count > 0);
//        }
//    }
//}