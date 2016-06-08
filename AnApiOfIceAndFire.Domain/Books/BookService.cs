using System;
using System.Linq;
using System.Linq.Expressions;
using AnApiOfIceAndFire.Data.Entities;
using Geymsla;

namespace AnApiOfIceAndFire.Domain.Books
{
    public class BookService : BaseService<IBook, BookEntity, BookFilter>
    {
        private static readonly Expression<Func<BookEntity, object>>[] BookIncludeProperties =
        {
            book => book.Characters,
            book => book.PovCharacters
        };

        public BookService(IReadOnlyRepository<BookEntity, int> repository) : base(repository, BookIncludeProperties) { }

        protected override IBook CreateModel(BookEntity entity)
        {
            return new BookEntityAdapter(entity);
        }

        protected override Func<IQueryable<BookEntity>, IQueryable<BookEntity>> CreatePredicate(BookFilter filter)
        {
            Func<IQueryable<BookEntity>, IQueryable<BookEntity>> bookFilters = bookEntities =>
            {
                if (filter.Name != null)
                {
                    bookEntities = bookEntities.Where(x => x.Name.Equals(filter.Name));
                }
                if (filter.FromReleaseDate.HasValue)
                {
                    bookEntities = bookEntities.Where(x => x.ReleaseDate >= filter.FromReleaseDate.Value);
                }
                if (filter.ToReleaseDate.HasValue)
                {
                    bookEntities = bookEntities.Where(x => x.ReleaseDate <= filter.ToReleaseDate.Value);
                }

                return bookEntities;
            };

            return bookFilters;
        }
    }
}