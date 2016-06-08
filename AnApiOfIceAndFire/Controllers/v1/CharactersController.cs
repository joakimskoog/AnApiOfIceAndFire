using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using AnApiOfIceAndFire.Domain;
using AnApiOfIceAndFire.Domain.Characters;
using AnApiOfIceAndFire.Infrastructure.Links;
using AnApiOfIceAndFire.Models.v1;
using AnApiOfIceAndFire.Models.v1.Mappers;

namespace AnApiOfIceAndFire.Controllers.v1
{
    [RoutePrefix("api/characters")]
    public class CharactersController : BaseController<ICharacter, Character, CharacterFilter>
    {
        public CharactersController(IModelService<ICharacter, CharacterFilter> modelService, IModelMapper<ICharacter, Character> modelMapper, IPagingLinksFactory<CharacterFilter> pagingLinksFactory)
            : base(modelService, modelMapper, pagingLinksFactory)
        {
        }

        [Route("{id:int}", Name = CharacterLinkCreator.SingleCharacterRouteName)]
        [HttpGet]
        public async Task<IHttpActionResult> Get(int id)
        {
            return await GetSingle(id);
        }

        [Route("", Name = CharacterLinkCreator.MultipleCharactersRouteName)]
        [HttpHead]
        [HttpGet]
        public async Task<HttpResponseMessage> Get(int? page = DefaultPage, int? pageSize = DefaultPageSize, string name = null, string culture = null, string born = null, string died = null, bool? isAlive = null, Models.v1.Gender? gender = null)
        {
            var characterFilter = new CharacterFilter
            {
                Name = name,
                Born = born,
                Died = died,
                Culture = culture,
                IsAlive = isAlive,
                Gender = gender.ToDomainGender()
            };

            return await Get(page, pageSize, characterFilter);
        }
    }
}