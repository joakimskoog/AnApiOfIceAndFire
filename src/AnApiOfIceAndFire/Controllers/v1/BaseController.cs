using System;
using System.Linq;
using System.Threading.Tasks;
using AnApiOfIceAndFire.Data;
using AnApiOfIceAndFire.Infrastructure.Links;
using AnApiOfIceAndFire.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
// ReSharper disable StaticMemberInGenericType

namespace AnApiOfIceAndFire.Controllers.v1
{
    public class BaseController<TEntity, TEntityFilter, TExternalModel> : Controller where TEntity : BaseEntity where TEntityFilter : class
    {
        protected const int DefaultPage = 1;
        protected const int DefaultPageSize = 10;
        protected const int MaximumPageSize = 50;
        private static readonly TimeSpan CacheDuration = TimeSpan.FromMinutes(60);

        private readonly IEntityRepository<TEntity, TEntityFilter> _repository;
        private readonly IModelMapper<TEntity, TExternalModel> _modelMapper;
        private readonly IPagingLinksFactory<TEntityFilter> _pagingLinksFactory;
        private readonly IMemoryCache _memoryCache;
        private readonly string _cachePrefix;
                
        protected BaseController(IEntityRepository<TEntity, TEntityFilter> repository, IModelMapper<TEntity, TExternalModel> modelMapper, IPagingLinksFactory<TEntityFilter> pagingLinksFactory, IMemoryCache memoryCache, string cachePrefix)
        {
            _pagingLinksFactory = pagingLinksFactory ?? throw new ArgumentNullException(nameof(pagingLinksFactory));
            _memoryCache = memoryCache ?? throw new ArgumentNullException(nameof(memoryCache));
            _cachePrefix = cachePrefix ?? throw new ArgumentNullException(nameof(cachePrefix));
            _modelMapper = modelMapper ?? throw new ArgumentNullException(nameof(modelMapper));
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        protected async Task<IActionResult> GetSingle(int id)
        {
            var key = $"{_cachePrefix}_{id}";
            
            if (_memoryCache.TryGetValue(key, out TExternalModel mappedEntity))
            {
                if (mappedEntity != null)
                {
                    return Ok(mappedEntity);
                }

                return NotFound();
            }

            var entity = await _repository.GetEntityAsync(id);
            var mappedModel = _modelMapper.Map(entity, Url);
            _memoryCache.Set(key, mappedModel, CacheDuration);

            if (mappedModel == null)
            {
                return NotFound();
            }

            return Ok(mappedModel);
        }

        protected async Task<IActionResult> Get(int? page = DefaultPage, int? pageSize = DefaultPageSize, TEntityFilter filter = null)
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
            var pagingLinks = _pagingLinksFactory.Create(pagedModels, Url, filter);

            Response.Headers.AddLinkHeader(pagingLinks);
            
            return Ok(mappedModels);
        }
    }
}


