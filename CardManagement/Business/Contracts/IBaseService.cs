

using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore.Query;

namespace Business.Contracts
{
    /// <summary>
    /// Generic service contract for CRUD operations.
    /// </summary>
    /// <typeparam name="TEntity">The entity type.</typeparam>
    /// <typeparam name="TDto">The DTO type used for creation.</typeparam>
    /// <typeparam name="TUpdateDto">The DTO type used for updates.</typeparam>
    public interface IBaseService<TEntity, TDto, TUpdateDto>
    where TEntity : class
    where TDto : class
    {
        /// <summary>
        /// Retrieves a list of entities matching the specified criteria.
        /// </summary>
        /// <param name="predicate">A filter expression to apply to the query.</param>
        /// <param name="orderBy">A function to define the order of the results.</param>
        /// <param name="include">A function to include related entities in the query.</param>
        /// <returns>A list of matching entities.</returns>
        Task<List<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>>? predicate = null,
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
        Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? include = null);
        /// <summary>
        /// Retrieves an entity by its unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the entity.</param>
        /// <param name="include">A function to include related entities in the query.</param>
        /// <returns>The entity matching the given identifier.</returns>
        Task<TEntity> GetByIdAsync(Guid id, Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? include = null);
        /// <summary>
        /// Creates a new entity using the provided DTO.
        /// </summary>
        /// <param name="dto">The data transfer object containing creation data.</param>
        Task CreateAsync(TDto dto);
        /// <summary>
        /// Updates an existing entity using the provided DTO and identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the entity to update.</param>
        /// <param name="dto">The data transfer object containing updated data.</param>
        /// <param name="include">A function to include related entities in the query.</param>
        Task UpdateAsync(Guid id, TUpdateDto dto, Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? include = null);
    }
}
