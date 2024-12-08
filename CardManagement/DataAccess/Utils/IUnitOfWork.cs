using DataAccess.Repositories.Base;

namespace DataAccess.Utils
{
    public interface IUnitOfWork : IDisposable
    {
        /// <summary>
        /// Retrieves a generic repository for the specified entity type.
        /// </summary>
        /// <typeparam name="TEntity">The type of the entity.</typeparam>
        /// <returns>A repository for the specified entity type.</returns>
        IBaseRepository<TEntity> GetRepository<TEntity>() where TEntity : class;

        /// <summary>
        /// Saves all changes made in the current context to the database.
        /// </summary>
        /// <returns>The number of state entries written to the database.</returns>
        Task<int> SaveChangesAsync(string currentUser);

        /// <summary>
        /// Executes the given set of operations within a database transaction.
        /// If an error occurs, all changes are rolled back.
        /// </summary>
        /// <param name="action">The asynchronous operations to execute within the transaction.</param>
        Task ExecuteInTransactionAsync(Func<Task> action, string currentUser);

        /// <summary>
        /// Updates audit fields such as CreatedBy and CreatedTime for added entities.
        /// </summary>
        void UpdateAuditFields(string currentUser);
    }
}
