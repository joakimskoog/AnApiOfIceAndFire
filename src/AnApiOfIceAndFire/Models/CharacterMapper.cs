using System;
using System.Linq;
using AnApiOfIceAndFire.Data.Characters;
using AnApiOfIceAndFire.Infrastructure.Links;
using Microsoft.AspNetCore.Mvc;

namespace AnApiOfIceAndFire.Models
{
    public class CharacterMapper : IModelMapper<CharacterModel, Character>
    {
        public Character Map(CharacterModel @from, IUrlHelper urlHelper)
        {
            if (@from == null) return null;
            if (urlHelper == null) throw new ArgumentNullException(nameof(urlHelper));

            var url = urlHelper.LinkToCharacter(from.Id);
            var fatherUrl = urlHelper.LinkToCharacter(from.FatherId);
            var motherUrl = urlHelper.LinkToCharacter(from.MotherId);
            var spouseUrl = urlHelper.LinkToCharacter(from.SpouseId);
            var bookUrls = from.BookIdentifiers.Select(urlHelper.LinkToBook).ToArray();
            var povBookUrls = from.PovBookIdentifiers.Select(urlHelper.LinkToBook).ToArray();
            var allegianceUrls = from.AllegianceIdentifiers.Select(urlHelper.LinkToHouse).ToArray();

            return new Character(url)
            {
                Name = from.Name ?? string.Empty,
                Gender = from.Gender,
                Culture = from.Culture ?? string.Empty,
                Born = from.Born ?? string.Empty,
                Died = from.Died ?? string.Empty,
                Father = fatherUrl,
                Mother = motherUrl,
                Spouse = spouseUrl,
                Books = bookUrls,
                PovBooks = povBookUrls,
                Aliases = from.Aliases?.Split(';', StringSplitOptions.RemoveEmptyEntries) ?? [],
                Titles = from.Titles?.Split(';', StringSplitOptions.RemoveEmptyEntries) ?? [],
                PlayedBy = from.PlayedBy?.Split(';', StringSplitOptions.RemoveEmptyEntries) ?? [],
                TvSeries = from.TvSeries?.Split(';', StringSplitOptions.RemoveEmptyEntries) ?? [],
                Allegiances = allegianceUrls
            };
        }
    }
}