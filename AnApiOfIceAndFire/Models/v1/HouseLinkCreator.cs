using System.Web.Http.Routing;
using AnApiOfIceAndFire.Domain.Models;

namespace AnApiOfIceAndFire.Models.v1
{
    public static class HouseLinkCreator
    {
        public const string SingleHouseRouteName = "HousesApi";
        public const string MultipleHousesRouteName = "MultipleHousesApi";

        public static string CreateHouseLink(IHouse house, UrlHelper urlHelper)
        {
            if (house == null) return string.Empty;

            return urlHelper.Link(SingleHouseRouteName, new {id = house.Identifier});
        }

        public static string CreateHousesLink(UrlHelper urlHelper)
        {
            if (urlHelper == null) return string.Empty;

            return urlHelper.Link(MultipleHousesRouteName, new { });
        }
    }
}