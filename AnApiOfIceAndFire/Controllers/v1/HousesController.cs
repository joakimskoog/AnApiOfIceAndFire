using AnApiOfIceAndFire.Domain.Models;
using AnApiOfIceAndFire.Domain.Services;
using AnApiOfIceAndFire.Models.v0;
using AnApiOfIceAndFire.Models.v0.Mappers;

namespace AnApiOfIceAndFire.Controllers.v1
{
    public class HousesController : BaseController<IHouse, House>
    {
        public HousesController(IModelService<IHouse> modelService, IModelMapper<IHouse, House> modelMapper) : base(modelService, modelMapper, HouseLinkCreator.HouseRouteName)
        {
        }
    }
}