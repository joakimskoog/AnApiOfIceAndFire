using System;
using System.Threading.Tasks;
using AnApiOfIceAndFire.Data;
using Microsoft.AspNetCore.Mvc;

namespace AnApiOfIceAndFire.Controllers.v1
{
    public class BaseController<TEntity, TEntityFilter> : Controller where TEntity : BaseEntity where TEntityFilter : class
    {
        protected const int DefaultPage = 1;
        protected const int DefaultPageSize = 10;
        protected const int MaximumPageSize = 50;

        private readonly IEntityRepository<TEntity, TEntityFilter> _repository;
        
        protected BaseController(IEntityRepository<TEntity, TEntityFilter> repository)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        protected virtual async Task<IActionResult> GetSingle(int id)
        {
            var entity = await _repository.GetEntityAsync(id);
            if (entity == null)
            {
                return NotFound();
            }

            //Map to external model here

            return Ok(entity);
        }
    }
}