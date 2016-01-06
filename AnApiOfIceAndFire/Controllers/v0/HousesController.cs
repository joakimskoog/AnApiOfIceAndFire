using AnApiOfIceAndFire.Domain;
using AnApiOfIceAndFire.Domain.Models;
using AnApiOfIceAndFire.Models.v0;
using AnApiOfIceAndFire.Models.v0.Mappers;

namespace AnApiOfIceAndFire.Controllers.v0
{
    public class HousesController : BaseController<IHouse, House>
    {
        public HousesController(IModelService<IHouse> modelService, IModelMapper<IHouse, House> modelMapper) : base(modelService, modelMapper, "HousesApi")
        {
        }
    }
}