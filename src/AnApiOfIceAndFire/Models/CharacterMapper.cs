using System;
using System.Linq;
using AnApiOfIceAndFire.Data.Characters;
using AnApiOfIceAndFire.Infrastructure.Links;
using Microsoft.AspNetCore.Mvc;

namespace AnApiOfIceAndFire.Models
{
    public class CharacterMapper : IModelMapper<CharacterEntity, Character>
    {
        public Character Map(CharacterEntity @from, IUrlHelper urlHelper)
        {
            if (@from == null) throw new ArgumentNullException(nameof(@from));
            if (urlHelper == null) throw new ArgumentNullException(nameof(urlHelper));

            var url = urlHelper.LinkToCharacter(from.Id);
            var fatherUrl = urlHelper.LinkToCharacter(from.FatherId);
            var motherUrl = urlHelper.LinkToCharacter(from.MotherId);
            var spouseUrl = urlHelper.LinkToCharacter(from.SpouseId);
            var bookUrls = from.BookIdentifiers.Select(urlHelper.LinkToBook);
            var povBookUrls = from.PovBookIdentifiers.Select(urlHelper.LinkToBook);
            var allegianceUrls = from.AllegianceIdentifiers.Select(urlHelper.LinkToHouse);

            return new Character(url)
            {
                Name = from.Name ?? string.Empty,
                Gender = from.IsFemale ? Gender.Female : Gender.Male,
                Culture = from.Culture ?? string.Empty,
                Born = from.Born ?? string.Empty,
                Died = from.Died ?? string.Empty,
                Father = fatherUrl,
                Mother = motherUrl,
                Spouse = spouseUrl,
                Books = bookUrls,
                PovBooks = povBookUrls,
                Aliases = from.ParseAliases(),
                Titles = from.ParseTitles(),
                PlayedBy = from.ParsePlayedBy(),
                TvSeries = from.ParseTvSeries(),
                Allegiances = allegianceUrls
            };
        }
    }
}