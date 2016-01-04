using AnApiOfIceAndFire.Domain;
using AnApiOfIceAndFire.Domain.Models;
using AnApiOfIceAndFire.Models.v0;
using AnApiOfIceAndFire.Models.v0.Mappers;

namespace AnApiOfIceAndFire.Controllers.v0
{
    public class BooksController : BaseController<IBook, Book>
    {
        public BooksController(IModelService<IBook> modelService, IModelMapper<IBook, Book> modelMapper) : base(modelService, modelMapper, "BooksApi") { }
    }
}