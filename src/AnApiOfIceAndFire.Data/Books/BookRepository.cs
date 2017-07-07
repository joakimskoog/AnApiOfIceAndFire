using System.Threading.Tasks;
using SimplePagedList;

namespace AnApiOfIceAndFire.Data.Books
{
    public class BookRepository : IEntityRepository<BookEntity,BookFilter>
    {
        public Task<BookEntity> GetEntityAsync(int id)
        {
            throw new System.NotImplementedException();
        }

        public IPagedList<BookEntity> GetPaginatedEntitiesAsync(int page, int pageSize, BookFilter filter = null)
        {
            throw new System.NotImplementedException();
        }
    }
}