using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using AnApiOfIceAndFire.Domain;
using AnApiOfIceAndFire.Domain.Houses;
using AnApiOfIceAndFire.Infrastructure.Links;
using AnApiOfIceAndFire.Models.v1;
using AnApiOfIceAndFire.Models.v1.Mappers;

namespace AnApiOfIceAndFire.Controllers.v1
{
    [RoutePrefix("api/houses")]
    public class HousesController : BaseController<IHouse, House, HouseFilter>
    {
        public HousesController(IModelService<IHouse, HouseFilter> modelService, IModelMapper<IHouse, House> modelMapper, IPagingLinksFactory<HouseFilter> pagingLinksFactory)
            : base(modelService, modelMapper, pagingLinksFactory)
        {
        }

        [Route("{id:int}", Name = HouseLinkCreator.SingleHouseRouteName)]
        [HttpGet]
        public async Task<IHttpActionResult> Get(int id)
        {
            return await GetSingle(id);
        }

        [Route("", Name = HouseLinkCreator.MultipleHousesRouteName)]
        [HttpHead]
        [HttpGet]
        public async Task<HttpResponseMessage> Get(int? page = DefaultPage, int? pageSize = DefaultPageSize, string name = null, string region = null, string words = null, bool? hasWords = null, bool? hasTitles = null,
            bool? hasSeats = null, bool? hasDiedOut = null, bool? hasAncestralWeapons = null)
        {
            var houseFilter = new HouseFilter
            {
                Name = name,
                Region = region,
                Words = words,
                HasAncestralWeapons = hasAncestralWeapons,
                HasDiedOut = hasDiedOut,
                HasWords = hasWords,
                HasSeats = hasSeats,
                HasTitles = hasTitles
            };

            return await Get(page, pageSize, houseFilter);
        }
    }
}