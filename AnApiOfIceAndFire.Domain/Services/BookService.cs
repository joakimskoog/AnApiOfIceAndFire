using System;
using System.Linq.Expressions;
using AnApiOfIceAndFire.Data;
using AnApiOfIceAndFire.Data.Entities;
using AnApiOfIceAndFire.Domain.Adapters;
using AnApiOfIceAndFire.Domain.Models;

namespace AnApiOfIceAndFire.Domain.Services
{
    public class BookService : BaseService<IBook, BookEntity>
    {
        private static readonly Expression<Func<BookEntity, object>>[] BookIncludeProperties =
        {
            book => book.Characters,
            book => book.PovCharacters
        };

        public BookService(IRepositoryWithIntKey<BookEntity> repository) : base(repository, BookIncludeProperties) { }

        protected override IBook CreateModel(BookEntity entity)
        {
            return new BookEntityAdapter(entity);
        }
    }
}