using System;
using System.Linq;
using System.Web.Http.Routing;
using AnApiOfIceAndFire.Domain.Houses;

namespace AnApiOfIceAndFire.Models.v1.Mappers
{
    public class HouseMapper : IModelMapper<IHouse, House>
    {
        public House Map(IHouse input, UrlHelper urlHelper)
        {
            if (input == null) throw new ArgumentNullException(nameof(input));
            if (urlHelper == null) throw new ArgumentNullException(nameof(urlHelper));

            var url = HouseLinkCreator.CreateHouseLink(input, urlHelper);
            var currentLordUrl = CharacterLinkCreator.CreateCharacterLink(input.CurrentLord, urlHelper);
            var heirUrl = CharacterLinkCreator.CreateCharacterLink(input.Heir, urlHelper);
            var founderUrl = CharacterLinkCreator.CreateCharacterLink(input.Founder, urlHelper);
            var swornMembersUrls = input.SwornMembers?.Select(c => CharacterLinkCreator.CreateCharacterLink(c, urlHelper));
            var overlordUrl = HouseLinkCreator.CreateHouseLink(input.Overlord, urlHelper);
            var cadetBranchesUrls = input.CadetBranches?.Select(h => HouseLinkCreator.CreateHouseLink(h, urlHelper));

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
    }
}