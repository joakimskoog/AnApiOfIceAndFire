using System.Web.Http.Routing;
using AnApiOfIceAndFire.Domain.Models;

namespace AnApiOfIceAndFire.Models.v1
{
    public static class HouseLinkCreator
    {
        public const string HouseRouteName = "HousesApi";

        public static string CreateHouseLink(IHouse house, UrlHelper urlHelper)
        {
            if (house == null) return string.Empty;

            return urlHelper.Link(HouseRouteName, new {id = house.Identifier});
        }
    }
}