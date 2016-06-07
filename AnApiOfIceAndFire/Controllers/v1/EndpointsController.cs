using System.Web.Http;
using AnApiOfIceAndFire.Models.v1;

namespace AnApiOfIceAndFire.Controllers.v1
{
    public class EndpointsController : ApiController
    {
        [Route("api", Name = "EndpointsApi")]
        [HttpGet]
        public IHttpActionResult Get()
        {
            var booksUrl = BookLinkCreator.CreateBooksLink(Url);
            var charactersUrl = CharacterLinkCreator.CreateCharactersLink(Url);
            var housesUrl = HouseLinkCreator.CreateHousesLink(Url);

            return Ok(new Endpoints
            {
                Books = booksUrl,
                Characters = charactersUrl,
                Houses = housesUrl
            });
        }
    }
}