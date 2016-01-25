using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using AnApiOfIceAndFire.Domain.Services;
using AnApiOfIceAndFire.Infrastructure.Links;
using AnApiOfIceAndFire.Models.v0.Mappers;

namespace AnApiOfIceAndFire.Controllers.v1
{
    public abstract class BaseController<TModel, TOutputModel> : ApiController
    {
        public const int DefaultPage = 1;
        public const int DefaultPageSize = 10;
        public const int MaximumPageSize = 50;

        private readonly IModelService<TModel> _modelService;
        private readonly IModelMapper<TModel, TOutputModel> _modelMapper;
        private readonly string _routeName;

        protected BaseController(IModelService<TModel> modelService, IModelMapper<TModel, TOutputModel> modelMapper, string routeName)
        {
            if (modelService == null) throw new ArgumentNullException(nameof(modelService));
            if (modelMapper == null) throw new ArgumentNullException(nameof(modelMapper));
            if (routeName == null) throw new ArgumentNullException(nameof(routeName));
            _modelService = modelService;
            _modelMapper = modelMapper;
            _routeName = routeName;
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

        [HttpHead]
        [HttpGet]
        public virtual async Task<HttpResponseMessage> Get(int? page = DefaultPage, int? pageSize = DefaultPageSize)
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

            var pagedModels = await _modelService.GetPaginatedAsync(page.Value, pageSize.Value);
            var mappedModels = pagedModels.Select(pm => _modelMapper.Map(pm, Url));
            var pagingLinks = pagedModels.ToPagingLinks(Url, _routeName);

            var response = Request.CreateResponse(HttpStatusCode.OK, mappedModels);
            response.Headers.AddLinkHeader(pagingLinks);

            return response;
        }
    }
}