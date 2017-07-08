using AnApiOfIceAndFire.Models;
using Microsoft.AspNetCore.Mvc;

namespace AnApiOfIceAndFire.Controllers.v1
{
    [ApiVersion("1")]
    public class EndpointsController : Controller
    {
        [HttpGet]
        [Route("api", Name = "EndpointsResource")]
        public IActionResult Get()
        {
            var booksUrl = Url.Link(BooksController.MultipleBooksRouteName, new { });
            var charactersUrl = Url.Link(CharactersController.MultipleCharactersRouteName, new { });
            var housesUrl = Url.Link(HousesController.MultipleHousesRouteName, new { });

            var endpoints = new Endpoints(booksUrl, charactersUrl, housesUrl);

            return Ok(endpoints);
        }
    }
} 