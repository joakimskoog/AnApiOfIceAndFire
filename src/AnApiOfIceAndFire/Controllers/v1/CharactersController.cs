using System;
using System.Threading.Tasks;
using AnApiOfIceAndFire.Data;
using AnApiOfIceAndFire.Data.Characters;
using AnApiOfIceAndFire.Infrastructure.Links;
using AnApiOfIceAndFire.Models;
using Microsoft.AspNetCore.Mvc;

namespace AnApiOfIceAndFire.Controllers.v1
{
    [ApiVersion("1")]
    [Route("api/characters")]
    public class CharactersController : BaseController<CharacterEntity, CharacterFilter, Character>
    {
        public const string SingleCharacterRouteName = "SingleCharacterEndpoint";
        public const string MultipleCharactersRouteName = "MultipleCharactersEndpoint";

        public CharactersController(IEntityRepository<CharacterEntity, CharacterFilter> repository, IModelMapper<CharacterEntity, Character> modelMapper, IPagingLinksFactory<CharacterFilter> pagingLinksFactory) : base(repository, modelMapper, pagingLinksFactory)
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
        public async Task<IActionResult> Get(int? page = DefaultPage, int? pageSize = DefaultPageSize, string name = null, string culture = null, string born = null, string died = null, bool? isAlive = null, Gender? gender = null)
        {
            var characterFilter = new CharacterFilter()
            {
                Name = name,
                Gender = gender,
                Culture = culture,
                Born = born,
                Died = died,
                IsAlive = isAlive
            };

            return await Get(page, pageSize, characterFilter);
        }
    }
}