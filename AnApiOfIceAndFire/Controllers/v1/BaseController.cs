using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using AnApiOfIceAndFire.Domain.Services;
using AnApiOfIceAndFire.Infrastructure.Links;
using AnApiOfIceAndFire.Models.v1.Mappers;

namespace AnApiOfIceAndFire.Controllers.v1
{
    public abstract class BaseController<TModel, TOutputModel, TFilter> : ApiController where TFilter : class
    {
        public const int DefaultPage = 1;
        public const int DefaultPageSize = 10;
        public const int MaximumPageSize = 50;

        private readonly IModelService<TModel, TFilter> _modelService;
        private readonly IModelMapper<TModel, TOutputModel> _modelMapper;
        private readonly IPagingLinksFactory<TFilter> _pagingLinksFactory;


        protected BaseController(IModelService<TModel, TFilter> modelService, IModelMapper<TModel, TOutputModel> modelMapper, IPagingLinksFactory<TFilter> pagingLinksFactory)
        {
            if (modelService == null) throw new ArgumentNullException(nameof(modelService));
            if (modelMapper == null) throw new ArgumentNullException(nameof(modelMapper));
            if (pagingLinksFactory == null) throw new ArgumentNullException(nameof(pagingLinksFactory));
            _modelService = modelService;
            _modelMapper = modelMapper;
            _pagingLinksFactory = pagingLinksFactory;
        }

        [HttpGet]
        public virtual async Task<IHttpActionResult> Get(int id)
        {
            var model = await _modelService.GetAsync(id);
            if (model == null)
            {
                return NotFound();
            }

            var mappedModel = _modelMapper.Map(model, Url);

            return Ok(mappedModel);
        }

        protected async Task<HttpResponseMessage> Get(int? page = DefaultPage, int? pageSize = DefaultPageSize, TFilter filter = null)
        {
            if (page == null)
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

            var pagedModels = await _modelService.GetPaginatedAsync(page.Value, pageSize.Value, filter);
            var mappedModels = pagedModels.Select(pm => _modelMapper.Map(pm, Url));
            var pagingLinks = _pagingLinksFactory.Create(pagedModels, Url, filter);

            var response = Request.CreateResponse(HttpStatusCode.OK, mappedModels);
            response.Headers.AddLinkHeader(pagingLinks);

            return response;
        }
    }
}