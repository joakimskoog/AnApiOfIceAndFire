using System;
using System.Linq;
using System.Web.Http.Routing;
using AnApiOfIceAndFire.Domain.Characters;

namespace AnApiOfIceAndFire.Models.v1.Mappers
{
    public class CharacterMapper : IModelMapper<ICharacter, Character>
    {
        private readonly IModelMapper<Domain.Characters.Gender, Gender> _genderMapper;

        public CharacterMapper(IModelMapper<Domain.Characters.Gender, Gender> genderMapper)
        {
            if (genderMapper == null) throw new ArgumentNullException(nameof(genderMapper));
            _genderMapper = genderMapper;
        }

        public Character Map(ICharacter input, UrlHelper urlHelper)
        {
            if (input == null) throw new ArgumentNullException(nameof(input));
            if (urlHelper == null) throw new ArgumentNullException(nameof(urlHelper));

            var url = CharacterLinkCreator.CreateCharacterLink(input, urlHelper);
            var fatherUrl = CharacterLinkCreator.CreateCharacterLink(input.Father, urlHelper);
            var motherUrl = CharacterLinkCreator.CreateCharacterLink(input.Mother, urlHelper);
            var spouseUrl = CharacterLinkCreator.CreateCharacterLink(input.Spouse, urlHelper);
            var booksUrls = input.Books?.Select(b => BookLinkCreator.CreateBookLink(b, urlHelper));
            var povBooksUrls = input.PovBooks?.Select(b => BookLinkCreator.CreateBookLink(b, urlHelper));
            var allegiances = input.Allegiances?.Select(h => HouseLinkCreator.CreateHouseLink(h, urlHelper));

            return new Character(url)
            {
                Name = input.Name ?? string.Empty,
                Gender = _genderMapper.Map(input.Gender, urlHelper),
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