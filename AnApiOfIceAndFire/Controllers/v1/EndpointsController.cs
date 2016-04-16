using System.Web.Http;
using AnApiOfIceAndFire.Models.v1;

namespace AnApiOfIceAndFire.Controllers.v1
{
    public class EndpointsController : ApiController
    {
        [HttpGet]
        public IHttpActionResult Get()
        {
            var booksUrl = Url.Link(BookLinkCreator.BookRouteName, new { });
            var charactersUrl = Url.Link(CharacterLinkCreator.CharacterRouteName, new { });
            var housesUrl = Url.Link(HouseLinkCreator.HouseRouteName, new { });

            return Ok(new Endpoints
            {
                Books = booksUrl,
                Characters = charactersUrl,
                Houses = housesUrl
            });
        }
    }
}