using System;

namespace AnApiOfIceAndFire.Models.v1
{
    public static class GenderExtensions
    {
        public static Domain.Characters.Gender? ToDomainGender(this Gender? gender)
        {
            if (!gender.HasValue) return null;

            switch (gender)
            {
                case Gender.Female: return Domain.Characters.Gender.Female;
                case Gender.Male: return Domain.Characters.Gender.Male;
                case Gender.Unknown: return Domain.Characters.Gender.Unknown;
                default: throw new ArgumentException($"Invalid Gender: {gender}");
            }
        }
    }
}