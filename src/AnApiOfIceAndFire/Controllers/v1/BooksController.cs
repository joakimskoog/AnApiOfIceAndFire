using System;
using System.Threading.Tasks;
using AnApiOfIceAndFire.Data;
using AnApiOfIceAndFire.Data.Books;
using AnApiOfIceAndFire.Models;
using Microsoft.AspNetCore.Mvc;

namespace AnApiOfIceAndFire.Controllers.v1
{
    [ApiVersion("1")]
    [Route("api/books")]
    public class BooksController : BaseController<BookEntity, BookFilter, Book>
    {
        public const string SingleBookRouteName = "SingleBookEndpoint";
        public const string MultipleBooksRouteName = "MultipleBooksEndpoint";

        public BooksController(IEntityRepository<BookEntity,BookFilter> repository, IModelMapper<BookEntity, Book> modelMapper) : base(repository, modelMapper)
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
        public IActionResult Get(int? page = DefaultPage, int? pageSize = DefaultPageSize, string name = null, DateTime? fromReleaseDate = null, DateTime? toReleaseDate = null)
        {
            var bookFilter = new BookFilter()
            {
                Name = name,
                FromReleaseDate = fromReleaseDate,
                ToReleaseDate = toReleaseDate
            };


            return null;
        }
    }
}