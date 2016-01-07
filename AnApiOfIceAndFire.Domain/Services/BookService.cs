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
        public BookService(IRepositoryWithIntKey<BookEntity> repository) : base(repository, new Expression<Func<BookEntity, object>>[0])
        {
        }

        protected override IBook CreateModel(BookEntity entity)
        {
            return new BookEntityAdapter(entity);
        }
    }
}