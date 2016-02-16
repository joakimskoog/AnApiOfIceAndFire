using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using AnApiOfIceAndFire.Domain.Models;
using AnApiOfIceAndFire.Domain.Models.Filters;
using AnApiOfIceAndFire.Domain.Services;
using AnApiOfIceAndFire.Models.v0;
using AnApiOfIceAndFire.Models.v0.Mappers;

namespace AnApiOfIceAndFire.Controllers.v1
{
    public class HousesController : BaseController<IHouse, House, HouseFilter>
    {
        public HousesController(IModelService<IHouse, HouseFilter> modelService, IModelMapper<IHouse, House> modelMapper) : base(modelService, modelMapper, HouseLinkCreator.HouseRouteName)
        {
        }

        [HttpHead]
        [HttpGet]
        public async Task<HttpResponseMessage> Get(int? page = DefaultPage, int? pageSize = DefaultPageSize, string name = "", string region = "", string words = "", bool? hasWords = null, bool? hasTitles = null,
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