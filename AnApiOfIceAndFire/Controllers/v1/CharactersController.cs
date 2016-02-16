using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using AnApiOfIceAndFire.Domain.Models;
using AnApiOfIceAndFire.Domain.Models.Filters;
using AnApiOfIceAndFire.Domain.Services;
using AnApiOfIceAndFire.Models.v0;
using AnApiOfIceAndFire.Models.v0.Mappers;

namespace AnApiOfIceAndFire.Controllers.v1
{
    public class CharactersController : BaseController<ICharacter, Character, CharacterFilter>
    {
        public CharactersController(IModelService<ICharacter, CharacterFilter> modelService, IModelMapper<ICharacter, Character> modelMapper) : base(modelService, modelMapper, CharacterLinkCreator.CharacterRouteName)
        {
        }

        [HttpHead]
        [HttpGet]
        public async Task<HttpResponseMessage> Get(int? page = DefaultPage, int? pageSize = DefaultPageSize, string name = null, string culture = null, string born = null, string died = null, bool? isAlive = null)
        {
            var characterFilter = new CharacterFilter
            {
                Name = name,
                Born = born,
                Died = died,
                Culture = culture,
                IsAlive = isAlive
            };

            return await Get(page, pageSize, characterFilter);
        }
    }
}