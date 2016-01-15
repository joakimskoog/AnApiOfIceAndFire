using System;
using System.Linq;
using System.Web.Http.Routing;
using AnApiOfIceAndFire.Domain.Models;
using static AnApiOfIceAndFire.Models.v0.BookLinkCreator;
using static AnApiOfIceAndFire.Models.v0.CharacterLinkCreator;
using static AnApiOfIceAndFire.Models.v0.HouseLinkCreator;

namespace AnApiOfIceAndFire.Models.v0.Mappers
{
    public class CharacterMapper : IModelMapper<ICharacter, Character>
    {
        public Character Map(ICharacter input, UrlHelper urlHelper)
        {
            if (input == null) throw new ArgumentNullException(nameof(input));
            if (urlHelper == null) throw new ArgumentNullException(nameof(urlHelper));

            var url = CreateCharacterLink(input, urlHelper);
            var fatherUrl = CreateCharacterLink(input.Father, urlHelper);
            var motherUrl = CreateCharacterLink(input.Mother, urlHelper);
            var spouseUrl = CreateCharacterLink(input.Spouse, urlHelper);
            var booksUrls = input.Books?.Select(b => CreateBookLink(b, urlHelper));
            var povBooksUrls = input.PovBooks?.Select(b => CreateBookLink(b, urlHelper));
            var allegiances = input.Allegiances?.Select(h => CreateHouseLink(h, urlHelper));

            return new Character(url)
            {
                Name = input.Name ?? string.Empty,
                Culture = input.Culture ?? string.Empty,
                Born = input.Born ?? string.Empty,
                Died = input.Died ?? string.Empty,
                Father = fatherUrl,
                Mother = motherUrl,
                Spouse = spouseUrl,
                Books = booksUrls ?? Enumerable.Empty<string>(),
                PovBooks = povBooksUrls ?? Enumerable.Empty<string>(),
                Aliases = input.Aliases ?? Enumerable.Empty<string>(),
                Titles = input.Titles ?? Enumerable.Empty<string>(),
                PlayedBy = input.PlayedBy ?? Enumerable.Empty<string>(),
                TvSeries = input.TvSeries ?? Enumerable.Empty<string>(),
                Allegiances = allegiances ?? Enumerable.Empty<string>()
            };
        }
    }
}