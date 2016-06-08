using System.Web.Http.Routing;
using AnApiOfIceAndFire.Domain.Characters;

namespace AnApiOfIceAndFire.Models.v1
{
    public static class CharacterLinkCreator
    {
        public const string SingleCharacterRouteName = "SingleCharactersApi";
        public const string MultipleCharactersRouteName = "MultipleCharactersApi";

        public static string CreateCharacterLink(ICharacter character, UrlHelper urlHelper)
        {
            if (character == null) return string.Empty;

            return urlHelper.Link(SingleCharacterRouteName, new { id = character.Identifier });
        }

        public static string CreateCharactersLink(UrlHelper urlHelper)
        {
            if (urlHelper == null) return string.Empty;

            return urlHelper.Link(MultipleCharactersRouteName, new { });
        }

    }
}