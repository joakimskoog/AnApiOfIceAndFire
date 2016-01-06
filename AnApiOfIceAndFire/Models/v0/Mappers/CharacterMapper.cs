using System.Web.Http.Routing;
using AnApiOfIceAndFire.Domain.Models;

namespace AnApiOfIceAndFire.Models.v0.Mappers
{
    public class CharacterMapper : IModelMapper<ICharacter, Character>
    {
        public Character Map(ICharacter input, UrlHelper urlHelper)
        {
            throw new System.NotImplementedException();
        }
    }
}