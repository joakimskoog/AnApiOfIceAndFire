using System;
using System.Threading.Tasks;
using AnApiOfIceAndFire.Data;
using AnApiOfIceAndFire.Data.Characters;
using Microsoft.AspNetCore.Mvc;

namespace AnApiOfIceAndFire.Controllers.v1
{
    [ApiVersion("1")]
    [Route("api/characters")]
    public class CharactersController : BaseController<CharacterEntity, CharacterFilter>
    {
        public const string SingleCharacterRouteName = "SingleCharacterEndpoint";
        public const string MultipleCharactersRouteName = "MultipleCharactersEndpoint";

        public CharactersController(IEntityRepository<CharacterEntity, CharacterFilter> repository) : base(repository)
        {
            
        }

        [HttpGet]
        [Route("{id:int}", Name = SingleCharacterRouteName)]
        public async Task<IActionResult> Get(int id)
        {
            return await GetSingle(id);
        }

        [HttpGet]
        [HttpHead]
        [Route("", Name = MultipleCharactersRouteName)]
        public IActionResult Get(int? page = DefaultPage, int? pageSize = DefaultPageSize, string name = null, string culture = null, string born = null, string died = null, bool? isAlive = null, Gender? gender = null)
        {
            return Ok("MultipleCharacters");
        }
    }
}