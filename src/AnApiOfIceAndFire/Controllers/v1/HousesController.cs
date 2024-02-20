using System.Threading.Tasks;
using AnApiOfIceAndFire.Data;
using AnApiOfIceAndFire.Data.Houses;
using AnApiOfIceAndFire.Infrastructure.Links;
using AnApiOfIceAndFire.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace AnApiOfIceAndFire.Controllers.v1
{
    [Route("api/houses")]
    public class HousesController : BaseController<HouseEntity, HouseFilter, House>
    {
        public const string SingleHouseRouteName = "SingleHouseEndpoint";
        public const string MultipleHousesRouteName = "MultipleHousesEndpoint";


        public HousesController(IEntityRepository<HouseEntity, HouseFilter> repository, IModelMapper<HouseEntity, House> modelMapper, IPagingLinksFactory<HouseFilter> pagingLinksFactory, IMemoryCache memoryCache) : base(repository, modelMapper, pagingLinksFactory, memoryCache, "Houses")
        {
        }

        [HttpGet]
        [Route("{id:int}", Name = SingleHouseRouteName)]
        public async Task<IActionResult> Get(int id)
        {
            return await GetSingle(id);
        }

        [HttpGet]
        [HttpHead]
        [Route("", Name = MultipleHousesRouteName)]
        public async Task<IActionResult> Get(int? page = DefaultPage, int? pageSize = DefaultPageSize, string name = null, string region = null, string words = null,
            bool? hasWords = null, bool? hasTitles = null, bool? hasSeats = null, bool? hasDiedOut = null, bool? hasAncestralWeapons = null)
        {
            var houseFilter = new HouseFilter()
            {
                Name = name,
                Region = region,
                Words = words,
                HasAncestralWeapons = hasAncestralWeapons,
                HasDiedOut = hasDiedOut,
                HasSeats = hasSeats,
                HasTitles = hasTitles,
                HasWords = hasWords
            };

            return await Get(page, pageSize, houseFilter);
        }
    }
}