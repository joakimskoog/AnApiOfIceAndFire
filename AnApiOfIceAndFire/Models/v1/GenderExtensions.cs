using System;

namespace AnApiOfIceAndFire.Models.v1
{
    public static class GenderExtensions
    {
        public static Domain.Models.Gender? ToDomainGender(this Gender? gender)
        {
            if (!gender.HasValue) return null;

            switch (gender)
            {
                case Gender.Female: return Domain.Models.Gender.Female;
                case Gender.Male: return Domain.Models.Gender.Male;
                case Gender.Unknown: return Domain.Models.Gender.Unknown;
                default: throw new ArgumentException($"Invalid Gender: {gender}");
            }
        }
    }
}