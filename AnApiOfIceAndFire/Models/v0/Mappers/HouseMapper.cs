using System.Web.Http.Routing;
using AnApiOfIceAndFire.Domain.Models;

namespace AnApiOfIceAndFire.Models.v0.Mappers
{
    public class HouseMapper : IModelMapper<IHouse,House>
    {
        public House Map(IHouse input, UrlHelper urlHelper)
        {
            throw new System.NotImplementedException();
        }
    }
}