using System;
using System.Threading.Tasks;
using AnApiOfIceAndFire.Data;
using AnApiOfIceAndFire.Data.Houses;
using Microsoft.AspNetCore.Mvc;

namespace AnApiOfIceAndFire.Controllers.v1
{
    [ApiVersion("1")]
    [Route("api/houses")]
    public class HousesController : BaseController<HouseEntity, HouseFilter>
    {
        public const string SingleHouseRouteName = "SingleHouseEndpoint";
        public const string MultipleHousesRouteName = "MultipleHousesEndpoint";

        public HousesController(IEntityRepository<HouseEntity, HouseFilter> repository) : base(repository)
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
        public IActionResult Get(int? page = DefaultPage, int? pageSize = DefaultPageSize, string name = null, string region = null, string words = null,
            bool? hasWords = null, bool? hasTitles = null, bool? hasSeats = null, bool? hasDiedOut = null, bool? hasAncestralWeapons = null)
        {
            return Ok("MultipleHouses");
        }
    }
}