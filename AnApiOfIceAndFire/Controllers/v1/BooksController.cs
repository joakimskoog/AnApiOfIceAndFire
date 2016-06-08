using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using AnApiOfIceAndFire.Domain;
using AnApiOfIceAndFire.Domain.Books;
using AnApiOfIceAndFire.Infrastructure.Links;
using AnApiOfIceAndFire.Models.v1;
using AnApiOfIceAndFire.Models.v1.Mappers;

namespace AnApiOfIceAndFire.Controllers.v1
{
    [RoutePrefix("api/books")]
    public class BooksController : BaseController<IBook, Book, BookFilter>
    {
        public BooksController(IModelService<IBook, BookFilter> modelService, IModelMapper<IBook, Book> modelMapper, IPagingLinksFactory<BookFilter> pagingLinksFactory)
            : base(modelService, modelMapper, pagingLinksFactory)
        { }

        [Route("{id:int}", Name = BookLinkCreator.SingleBookRouteName)]
        [HttpGet]
        public async Task<IHttpActionResult> Get(int id)
        {
            return await GetSingle(id);
        }

        [Route("", Name = BookLinkCreator.MultipleBooksRouteName)]
        [HttpHead]
        [HttpGet]
        public async Task<HttpResponseMessage> Get(int? page = DefaultPage, int? pageSize = DefaultPageSize, string name = null, DateTime? fromReleaseDate = null, DateTime? toReleaseDate = null)
        {
            var bookFilter = new BookFilter
            {
                Name = name,
                FromReleaseDate = fromReleaseDate,
                ToReleaseDate = toReleaseDate
            };

            return await Get(page, pageSize, bookFilter);
        }
    }
}