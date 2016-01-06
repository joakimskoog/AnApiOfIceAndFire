using AnApiOfIceAndFire.Domain;
using AnApiOfIceAndFire.Domain.Models;
using AnApiOfIceAndFire.Models.v0;
using AnApiOfIceAndFire.Models.v0.Mappers;

namespace AnApiOfIceAndFire.Controllers.v0
{
    public class CharactersController : BaseController<ICharacter,Character>
    {
        public CharactersController(IModelService<ICharacter> modelService, IModelMapper<ICharacter, Character> modelMapper) : base(modelService, modelMapper, "CharactersApi")
        {
        }
    }
}