using System;
using System.Linq;
using System.Web.Http.Routing;
using AnApiOfIceAndFire.Domain.Models;

namespace AnApiOfIceAndFire.Models.v0.Mappers
{
    public class HouseMapper : IModelMapper<IHouse,House>
    {
        private const string HouseRouteName = "HousesApi";

        public House Map(IHouse input, UrlHelper urlHelper)
        {
            if (input == null) throw new ArgumentNullException(nameof(input));
            if (urlHelper == null) throw new ArgumentNullException(nameof(urlHelper));

            var url = CreateHouseUrl(input, urlHelper);
            var currentLordUrl = CreateCharacterUrl(input.CurrentLord, urlHelper);
            var heirUrl = CreateCharacterUrl(input.Heir, urlHelper);
            var founderUrl = CreateCharacterUrl(input.Founder, urlHelper);
            var swornMembersUrls = input.SwornMembers?.Select(c => CreateCharacterUrl(c, urlHelper));
            var overlordUrl = CreateHouseUrl(input.Overlord, urlHelper);
            var cadetBranchesUrls = input.CadetBranches?.Select(h => CreateHouseUrl(h, urlHelper));

            return new House(url, input.Name)
            {
                Region = input.Region ?? string.Empty,
                CoatOfArms = input.CoatOfArms ?? string.Empty,
                Words = input.Words ?? string.Empty,
                Founded = input.Founded ?? string.Empty,
                DiedOut = input.DiedOut ?? string.Empty,
                Titles = input.Titles ?? Enumerable.Empty<string>(),
                AncestralWeapons = input.AncestralWeapons ?? Enumerable.Empty<string>(),
                Seats = input.Seats ?? Enumerable.Empty<string>(),
                CurrentLord = currentLordUrl,
                Heir = heirUrl,
                Founder = founderUrl,
                SwornMembers = swornMembersUrls ?? Enumerable.Empty<string>(),
                Overlord = overlordUrl,
                CadetBranches = cadetBranchesUrls ?? Enumerable.Empty<string>()        
            };
        }

        private string CreateHouseUrl(IHouse house, UrlHelper urlHelper)
        {
            if (house == null) return string.Empty;

            return urlHelper.Link(HouseRouteName, new {id = house.Identifier});
        }

        private string CreateCharacterUrl(ICharacter character, UrlHelper urlHelper)
        {
            if (character == null) return string.Empty;

            return urlHelper.Link("CharactersApi", new {id = character.Identifier});
        }
    }
}