using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;
using Dapper.Contrib.Extensions;
using SimplePagedList;

namespace AnApiOfIceAndFire.Data
{
    public abstract class BaseRepository<TEntity, TEntityFilter> : IEntityRepository<TEntity, TEntityFilter> where TEntityFilter : class where TEntity : BaseEntity
    {
        protected readonly string ConnectionString;

        protected BaseRepository(string connectionString)
        {
            ConnectionString = connectionString ?? throw new ArgumentNullException(nameof(connectionString));
        }

        public abstract Task<TEntity> GetEntityAsync(int id);

        public abstract Task<IPagedList<TEntity>> GetPaginatedEntitiesAsync(int page, int pageSize, TEntityFilter filter = default(TEntityFilter));

        public async Task InsertEntitiesAsync(List<TEntity> entities)
        {
            using (var connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                using (var trans = connection.BeginTransaction())
                {
                    try
                    {
                        var insertTasks = new List<Task>();

                        foreach (var entity in entities)
                        {
                            insertTasks.Add(connection.InsertAsync(entity, trans));
                            insertTasks.Add(InsertRelationships(entity, trans, connection));
                        }

                        await Task.WhenAll(insertTasks);
                      
                        trans.Commit();
                    }
                    catch (Exception)
                    {
                        trans.Rollback();
                        throw;
                    }
                   
                }
            }
        }

        protected virtual Task InsertRelationships(TEntity entity, SqlTransaction transaction, SqlConnection connection)
        {
            return Task.CompletedTask;
        }
    }
}