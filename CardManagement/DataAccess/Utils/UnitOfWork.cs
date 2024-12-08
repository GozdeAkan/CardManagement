using DataAccess.Context;
using DataAccess.Repositories.Base;
using Domain.Entities.Base;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Utils
{
    /// <inheritdoc cref="IUnitOfWork" />
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _context;
        private readonly Dictionary<Type, object> _repositories;

        /// <summary>
        /// Initializes a new instance of the <see cref="UnitOfWork"/> class.
        /// </summary>
        /// <param name="context">The database context used for data operations.</param>
        public UnitOfWork(AppDbContext context)
        {
            _context = context;
            _repositories = new Dictionary<Type, object>();
        }

        /// <inheritdoc />
        public IBaseRepository<T> GetRepository<T>() where T : class
        {
            var type = typeof(T);

            if (!_repositories.ContainsKey(type))
            {
                var repositoryInstance = new BaseRepository<T>(_context);
                _repositories[type] = repositoryInstance;
            }

            return (IBaseRepository<T>)_repositories[type];
        }

        /// <inheritdoc />
        public async Task<int> SaveChangesAsync()
        {
            UpdateAuditFields();
            return await _context.SaveChangesAsync();
        }

        /// <inheritdoc />
        public async Task ExecuteInTransactionAsync(Func<Task> action)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();

            try
            {
                await action();
                UpdateAuditFields();
                await _context.SaveChangesAsync();
                await transaction.CommitAsync();
            }
            catch
            {
                await transaction.RollbackAsync();
                throw;
            }
        }

        /// <summary>
        /// Updates audit fields (CreatedBy, CreatedTime, UpdatedBy, UpdatedTime) for tracked entities.
        /// </summary>
        private void UpdateAuditFields()
        {
            var currentUser = "admin"; //_httpContextAccessor.HttpContext?.User?.Identity?.Name ?? "System";

            var addedEntries = _context.ChangeTracker.Entries()
                .Where(e => e.State == EntityState.Added && e.Entity is BaseEntity);

            foreach (var entry in addedEntries)
            {
                if (entry.Entity is BaseEntity baseEntity)
                {
                    baseEntity.CreatedBy = currentUser;
                    baseEntity.CreatedTime = DateTime.UtcNow;
                    baseEntity.UpdatedBy = currentUser;
                    baseEntity.UpdatedTime = DateTime.UtcNow;
                }
            }

            var modifiedEntries = _context.ChangeTracker.Entries()
                .Where(e => e.State == EntityState.Modified && e.Entity is BaseEntity);

            foreach (var entry in modifiedEntries)
            {
                if (entry.Entity is BaseEntity baseEntity)
                {
                    baseEntity.UpdatedBy = currentUser;
                    baseEntity.UpdatedTime = DateTime.UtcNow;

                    // Prevent overwriting CreatedBy and CreatedTime
                    entry.Property(nameof(BaseEntity.CreatedBy)).IsModified = false;
                    entry.Property(nameof(BaseEntity.CreatedTime)).IsModified = false;
                }
            }
        }

        /// <inheritdoc />
        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
