using System.Threading.Tasks;
using AnApiOfIceAndFire.Data;
using AnApiOfIceAndFire.Data.Characters;
using AnApiOfIceAndFire.Infrastructure.Links;
using AnApiOfIceAndFire.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace AnApiOfIceAndFire.Controllers.v1
{
    [Route("api/characters")]
    public class CharactersController : BaseController<CharacterModel, CharacterFilter, Character>
    {
        public const string SingleCharacterRouteName = "SingleCharacterEndpoint";
        public const string MultipleCharactersRouteName = "MultipleCharactersEndpoint";

        public CharactersController(IEntityRepository<CharacterModel, CharacterFilter> repository, IModelMapper<CharacterModel, Character> modelMapper, IPagingLinksFactory<CharacterFilter> pagingLinksFactory, IMemoryCache memoryCache) : 
            base(repository, modelMapper, pagingLinksFactory, memoryCache, "Characters")
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