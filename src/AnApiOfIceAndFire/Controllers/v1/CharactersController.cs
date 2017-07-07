using System;
using Microsoft.AspNetCore.Mvc;

namespace AnApiOfIceAndFire.Controllers.v1
{
    [ApiVersion("1")]
    [Route("api/characters")]
    public class CharactersController : Controller
    {
        public const string SingleCharacterRouteName = "SingleCharacterEndpoint";
        public const string MultipleCharactersRouteName = "MultipleCharactersEndpoint";

        [HttpGet]
        [Route("{id:int}", Name = SingleCharacterRouteName)]
        public IActionResult Get(int id)
        {
            return Ok("SingleCharacter");
        }

        [HttpGet]
        [HttpHead]
        [Route("", Name = MultipleCharactersRouteName)]
        public IActionResult Get()
        {
            return Ok("MultipleCharacters");
        }
    }
}