using System;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using AnApiOfIceAndFire.Data;
using AnApiOfIceAndFire.Infrastructure.Links;
using AnApiOfIceAndFire.Models;
using Microsoft.AspNetCore.Mvc;

namespace AnApiOfIceAndFire.Controllers.v1
{
    public class BaseController<TEntity, TEntityFilter, TExternalModel> : Controller where TEntity : BaseEntity where TEntityFilter : class
    {
        private readonly IModelMapper<TEntity, TExternalModel> _modelMapper;
        protected const int DefaultPage = 1;
        protected const int DefaultPageSize = 10;
        protected const int MaximumPageSize = 50;

        private readonly IEntityRepository<TEntity, TEntityFilter> _repository;

        protected BaseController(IEntityRepository<TEntity, TEntityFilter> repository, IModelMapper<TEntity, TExternalModel> modelMapper)
        {
            _modelMapper = modelMapper ?? throw new ArgumentNullException(nameof(modelMapper));
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        protected async Task<IActionResult> GetSingle(int id)
        {
            var entity = await _repository.GetEntityAsync(id);
            if (entity == null)
            {
                return NotFound();
            }

            var mappedModel = _modelMapper.Map(entity, Url);

            return Ok(mappedModel);
        }

        protected async Task<HttpResponseMessage> Get(int? page = DefaultPage, int? pageSize = DefaultPageSize, TEntityFilter filter = null)
        {
            if (page == null)
            {
                page = DefaultPage;
            }
            if (page < 1)
            {
                page = DefaultPage;
            }
            if (pageSize == null)
            {
                pageSize = DefaultPageSize;
            }
            if (pageSize > MaximumPageSize)
            {
                pageSize = MaximumPageSize;
            }

            var pagedModels = await _repository.GetPaginatedEntitiesAsync(page.Value, pageSize.Value, filter);
            var mappedModels = pagedModels.Select(pm => _modelMapper.Map(pm, Url));

            //Create paging links here
            //var pagingLinks = _pagingLinksFactory.Create(pagedModels, Url, filter);

            //var response = Request.CreateResponse(HttpStatusCode.OK, mappedModels);
            //response.Headers.AddLinkHeader(pagingLinks);

            //return response;

            return null;
        }
    }
}


