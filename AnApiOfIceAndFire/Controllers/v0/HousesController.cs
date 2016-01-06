using AnApiOfIceAndFire.Domain;
using AnApiOfIceAndFire.Domain.Models;
using AnApiOfIceAndFire.Models.v0;
using AnApiOfIceAndFire.Models.v0.Mappers;
using static AnApiOfIceAndFire.Models.v0.HouseLinkCreator;

namespace AnApiOfIceAndFire.Controllers.v0
{
    public class HousesController : BaseController<IHouse, House>
    {
        public HousesController(IModelService<IHouse> modelService, IModelMapper<IHouse, House> modelMapper) : base(modelService, modelMapper, HouseRouteName)
        {
        }
    }
}