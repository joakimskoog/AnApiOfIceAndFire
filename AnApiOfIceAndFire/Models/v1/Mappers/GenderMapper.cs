using System;
using System.Web.Http.Routing;

namespace AnApiOfIceAndFire.Models.v1.Mappers
{
    public class GenderMapper : IModelMapper<Domain.Models.Gender, Gender>
    {
        public Gender Map(Domain.Models.Gender input, UrlHelper urlHelper)
        {
            switch (input)
            {
                case Domain.Models.Gender.Female: return Gender.Female;
                case Domain.Models.Gender.Male: return Gender.Male;
                case Domain.Models.Gender.Unknown: return Gender.Unknown;
                default: throw new ArgumentException($"Invalid Gender: {input}");
            }
        }
    }
}