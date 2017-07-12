using System;
using System.Net.Http;
using System.Threading.Tasks;
using AnApiOfIceAndFire.Data;
using AnApiOfIceAndFire.Data.Books;
using AnApiOfIceAndFire.Infrastructure.Links;
using AnApiOfIceAndFire.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace AnApiOfIceAndFire.Controllers.v1
{
    [ApiVersion("1")]
    [Route("api/books")]
    public class BooksController : BaseController<BookEntity, BookFilter, Book>
    {
        public const string SingleBookRouteName = "SingleBookEndpoint";
        public const string MultipleBooksRouteName = "MultipleBooksEndpoint";

        public BooksController(IEntityRepository<BookEntity, BookFilter> repository, IModelMapper<BookEntity, Book> modelMapper, IPagingLinksFactory<BookFilter> pagingLinksFactory, IMemoryCache memoryCache) :
            base(repository, modelMapper, pagingLinksFactory, memoryCache, "Books")
        {
        }

        [HttpGet]
        [Route("{id:int}", Name = SingleBookRouteName)]
        public async Task<IActionResult> Get(int id)
        {
            return await GetSingle(id);
        }

        [HttpGet]
        [HttpHead]
        [Route("", Name = MultipleBooksRouteName)]
        public async Task<IActionResult> Get(int? page = DefaultPage, int? pageSize = DefaultPageSize, string name = null, DateTime? fromReleaseDate = null, DateTime? toReleaseDate = null)
        {
            var bookFilter = new BookFilter()
            {
                Name = name,
                FromReleaseDate = fromReleaseDate,
                ToReleaseDate = toReleaseDate
            };


            return await Get(page, pageSize, bookFilter);
        }
    }
}