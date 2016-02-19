using System.Web.Http.Routing;
using AnApiOfIceAndFire.Domain.Models;

namespace AnApiOfIceAndFire.Models.v1
{
    public static class CharacterLinkCreator
    {
        public const string CharacterRouteName = "CharactersApi";

        public static string CreateCharacterLink(ICharacter character, UrlHelper urlHelper)
        {
            if (character == null) return string.Empty;

            return urlHelper.Link(CharacterRouteName, new {id = character.Identifier});
        }
    }
}