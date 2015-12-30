using System;
using System.Collections.Generic;
using System.Web.Http;
using System.Web.Http.Description;
using AnApiOfIceAndFire.Models;
using AnApiOfIceAndFire.Models.v0;

namespace AnApiOfIceAndFire.Controllers
{
    public class BooksController : ApiController
    {
        [HttpGet]
        [ResponseType(typeof(Book))]
        public IHttpActionResult Get(int id)
        {
            throw new NotImplementedException();
        }

        [HttpGet]
        [ResponseType(typeof(PaginatedResponse<Book>))]
        public IHttpActionResult Get(int? page = null)
        {
            throw new NotImplementedException();
        }
    }
}