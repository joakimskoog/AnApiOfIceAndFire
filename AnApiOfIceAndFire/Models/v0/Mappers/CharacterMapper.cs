using System;
using System.Linq;
using System.Web.Http.Routing;
using AnApiOfIceAndFire.Domain.Models;

namespace AnApiOfIceAndFire.Models.v0.Mappers
{
    public class CharacterMapper : IModelMapper<ICharacter, Character>
    {
        private const string CharacterRouteName = "CharactersApi";

        public Character Map(ICharacter input, UrlHelper urlHelper)
        {
            if (input == null) throw new ArgumentNullException(nameof(input));
            if (urlHelper == null) throw new ArgumentNullException(nameof(urlHelper));

            var url = CreateCharacterUrl(input, urlHelper);
            var fatherUrl = CreateCharacterUrl(input.Father, urlHelper);
            var motherUrl = CreateCharacterUrl(input.Mother, urlHelper);
            var spouseUrl = CreateCharacterUrl(input.Spouse, urlHelper);
            var childrenUrls = input.Children?.Select(x => CreateCharacterUrl(x, urlHelper));
            var booksUrls = input.Books?.Select(b => CreateBookUrl(b, urlHelper));
            var povBooksUrls = input.PovBooks?.Select(b => CreateBookUrl(b, urlHelper));
            var allegiances = input.Allegiances?.Select(h => CreateHouseUrl(h, urlHelper));

            return new Character(url)
            {
                Name = input.Name ?? string.Empty,
                Culture = input.Culture ?? string.Empty,
                Born = input.Born ?? string.Empty,
                Died = input.Died ?? string.Empty,
                Father = fatherUrl,
                Mother = motherUrl,
                Spouse = spouseUrl,
                Children = childrenUrls ?? Enumerable.Empty<string>(),
                Books = booksUrls ?? Enumerable.Empty<string>(),
                PovBooks = povBooksUrls ?? Enumerable.Empty<string>(),
                Aliases = input.Aliases ?? Enumerable.Empty<string>(),
                Titles = input.Titles ?? Enumerable.Empty<string>(),
                PlayedBy = input.PlayedBy ?? Enumerable.Empty<string>(),
                TvSeries = input.TvSeries ?? Enumerable.Empty<string>(),
                Allegiances = allegiances ?? Enumerable.Empty<string>()
            };
        }

        private string CreateCharacterUrl(ICharacter character, UrlHelper urlHelper)
        {
            if (character == null) return string.Empty;

            return urlHelper.Link(CharacterRouteName, new { id = character.Identifier });
        }

        private string CreateBookUrl(IBook book, UrlHelper urlHelper)
        {
            if (book == null) return string.Empty;

            return urlHelper.Link("BooksApi", new { id = book.Identifier });
        }

        private string CreateHouseUrl(IHouse house, UrlHelper urlHelper)
        {
            if (house == null) return string.Empty;

            return urlHelper.Link("HousesApi", new { id = house.Identifier });
        }
    }
}