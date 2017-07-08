using System;
using System.Threading.Tasks;
using SimplePagedList;

namespace AnApiOfIceAndFire.Data.Books
{
    public class BookRepository : IEntityRepository<BookEntity,BookFilter>
    {
        private readonly string _connectionString;

        public BookRepository(string connectionString)
        {
            if (connectionString == null) throw new ArgumentNullException(nameof(connectionString));
            _connectionString = connectionString;
        }

        public Task<BookEntity> GetEntityAsync(int id)
        {
            throw new System.NotImplementedException();
        }
        public Task<IPagedList<BookEntity>> GetPaginatedEntitiesAsync(int page, int pageSize, BookFilter filter = null)
        {




            //filter.Name;
            //filter.FromReleaseDate;
            //filter.ToReleaseDate;

            throw new System.NotImplementedException();
        }
    }
}