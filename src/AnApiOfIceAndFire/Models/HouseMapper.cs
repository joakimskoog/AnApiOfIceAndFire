using System;
using System.Linq;
using AnApiOfIceAndFire.Data.Houses;
using AnApiOfIceAndFire.Infrastructure.Links;
using Microsoft.AspNetCore.Mvc;

namespace AnApiOfIceAndFire.Models
{
    public class HouseMapper : IModelMapper<HouseEntity, House>
    {
        public House Map(HouseEntity @from, IUrlHelper urlHelper)
        {
            if (@from == null) return null;
            if (urlHelper == null) throw new ArgumentNullException(nameof(urlHelper));

            var url = urlHelper.LinkToHouse(from.Id);
            var currentLordUrl = urlHelper.LinkToCharacter(from.CurrentLordId);
            var heirUrl = urlHelper.LinkToCharacter(from.HeirId);
            var founderUrl = urlHelper.LinkToCharacter(from.FounderId);
            var swornMemberUrls = from.SwornMemberIdentifiers.Select(urlHelper.LinkToCharacter);
            var overlordUrl = urlHelper.LinkToHouse(from.OverlordId);
            var cadetBranchUrls = from.CadetBranchIdentifiers.Select(urlHelper.LinkToHouse);

            return new House(url, from.Name)
            {
                Region = from.Region ?? string.Empty,
                CoatOfArms = from.CoatOfArms ?? string.Empty,
                Words = from.Words ?? string.Empty,
                Founded = from.Founded ?? string.Empty,
                DiedOut = from.DiedOut ?? string.Empty,
                Titles = from.ParseTitles(),
                AncestralWeapons = from.ParseAncestralWeapons(),
                Seats = from.ParseSeats(),
                CurrentLord = currentLordUrl,
                Heir = heirUrl,
                Founder = founderUrl,
                SwornMembers = swornMemberUrls ?? new string[0],
                Overlord = overlordUrl,
                CadetBranches = cadetBranchUrls ?? new string[0]
            };
        }
    }
}