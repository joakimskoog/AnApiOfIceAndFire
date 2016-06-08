using System;
using System.Web.Http.Routing;

namespace AnApiOfIceAndFire.Models.v1.Mappers
{
    public class GenderMapper : IModelMapper<Domain.Characters.Gender, Gender>
    {
        public Gender Map(Domain.Characters.Gender input, UrlHelper urlHelper)
        {
            switch (input)
            {
                case Domain.Characters.Gender.Female: return Gender.Female;
                case Domain.Characters.Gender.Male: return Gender.Male;
                case Domain.Characters.Gender.Unknown: return Gender.Unknown;
                default: throw new ArgumentException($"Invalid Gender: {input}");
            }
        }
    }
}