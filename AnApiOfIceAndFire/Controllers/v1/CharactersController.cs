using AnApiOfIceAndFire.Domain.Models;
using AnApiOfIceAndFire.Domain.Services;
using AnApiOfIceAndFire.Models.v0;
using AnApiOfIceAndFire.Models.v0.Mappers;

namespace AnApiOfIceAndFire.Controllers.v1
{
    public class CharactersController : BaseController<ICharacter,Character>
    {
        public CharactersController(IModelService<ICharacter> modelService, IModelMapper<ICharacter, Character> modelMapper) : base(modelService, modelMapper, CharacterLinkCreator.CharacterRouteName)
        {
        }
    }
}