using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace DataAccess.Repositories.Base
{
    /// <summary>
    /// Defines the base contract for a repository providing CRUD operations for entities.
    /// </summary>
    /// <typeparam name="TEntity">The type of the entity.</typeparam>
    public interface IBaseRepository<TEntity> where TEntity : class
    {
        /// <summary>
        /// Retrieves an entity by its unique identifier, with optional related entities included.
        /// </summary>
        /// <param name="id">The unique identifier of the entity.</param>
        /// <param name="include">A function to specify related entities to include in the query.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the entity.</returns>
        Task<TEntity> GetByIdAsync(Guid id, Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? include = null);

        /// <summary>
        /// Retrieves all entities that match the specified criteria, projected into a result type.
        /// </summary>
        /// <typeparam name="TResult">The type of the projection result.</typeparam>
        /// <param name="selector">A projection expression to select specific fields or objects.</param>
        /// <param name="predicate">An optional filter expression to apply to the entities.</param>
        /// <param name="include">An optional function to specify related entities to include in the query.</param>
        /// <param name="orderBy">An optional function to define the order of the results.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains a collection of the projected results.</returns>
        Task<IEnumerable<TResult>> GetAllAsync<TResult>(
            Expression<Func<TEntity, TResult>> selector,
            Expression<Func<TEntity, bool>>? predicate = null,
            Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? include = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null);

        /// <summary>
        /// Adds a new entity to the data source.
        /// </summary>
        /// <param name="entity">The entity to add.</param>
        /// <returns>A task that represents the asynchronous operation.</returns>
        Task AddAsync(TEntity entity);

        /// <summary>
        /// Updates an existing entity in the data source.
        /// </summary>
        /// <param name="entity">The entity to update.</param>
        /// <returns>A task that represents the asynchronous operation.</returns>
        Task UpdateAsync(TEntity entity);
    }
}
