using System.Linq;
using AnApiOfIceAndFire.Domain.Models;

namespace AnApiOfIceAndFire.Domain
{
    public interface IBookService
    {
        IBook GetBook(int id);

        IQueryable<IBook> GetAllBooks();
    }
}