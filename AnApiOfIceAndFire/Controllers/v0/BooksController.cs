using System.Collections.Generic;
using System.Web.Http;
using System.Web.Http.Description;
using AnApiOfIceAndFire.Domain;
using AnApiOfIceAndFire.Models.v0;

namespace AnApiOfIceAndFire.Controllers.v0
{
    public class BooksController : ApiController
    {
        private readonly IBookService _bookService;

        //public BooksController(IBookService bookService)
        //{
        //    if (bookService == null) throw new ArgumentNullException(nameof(bookService));
        //    _bookService = bookService;
        //}

        [HttpGet]
        [ResponseType(typeof(Book))]
        public IHttpActionResult Get(int id)
        {
            return Ok(0);
        }

        [HttpGet]
        [ResponseType(typeof(IEnumerable<Book>))]
        public IHttpActionResult Get(int? page = null, int? pageSize = null)
        {
            return Ok(0);
        }
    }
}