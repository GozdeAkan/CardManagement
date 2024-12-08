
using System.Linq.Expressions;
using Business.Contracts;
using DataAccess.Repositories.Base;
using DataAccess.Utils;
using Domain.Entities.Card;
using Infrastructure.Utils;
using Microsoft.EntityFrameworkCore.Query;

namespace Business.Services
{
    /// <inheritdoc cref="IBaseService{TEntity, TDto, TUpdateDto}" />
    public abstract class BaseService<TEntity, TDto, TUpdateDto> : IBaseService<TEntity, TDto, TUpdateDto>
    where TEntity : class
    where TDto : class
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IBaseRepository<TEntity> _repository;

        public BaseService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _repository = _unitOfWork.GetRepository<TEntity>();
        }
        /// <inheritdoc />
        public async Task<List<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>>? predicate = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null, Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? include = null)
        {
            var entities = await _repository.GetAllAsync(s => s, predicate, include, orderBy);
            return entities.ToList();
        }
        /// <inheritdoc />
        public async Task<TEntity> GetByIdAsync(Guid id, Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? include = null)
        {
            var entity = await _repository.GetByIdAsync(id, include);
            if (entity == null)
                throw new Exception($"Card with ID {id} not found.");
            return entity;
        }
        /// <inheritdoc />
        public async Task CreateAsync(TDto dto)
        {
            var entity = MapToEntity(dto);
            await _repository.AddAsync(entity);
            await _unitOfWork.SaveChangesAsync(WorkContext.CurrentEmailAddress);
        }
        /// <inheritdoc />
        public async Task UpdateAsync(Guid id, TUpdateDto dto, Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? include = null)
        {
            await _unitOfWork.ExecuteInTransactionAsync(async () => //If anything goes wrong during the process, no changes are applied to the database.
            {
                var entity = await _repository.GetByIdAsync(id, include);
                if (entity == null)
                    throw new KeyNotFoundException("Entity not found");

                var updatedEntity = MapDtoToEntity(dto, entity);
                await _repository.UpdateAsync(updatedEntity);
            }, WorkContext.CurrentEmailAddress);
        }
        /// <summary>
        /// Maps the provided DTO to an entity.
        /// </summary>
        /// <param name="dto">The DTO containing data to map to an entity.</param>
        /// <returns>The mapped entity.</returns>
        public abstract TEntity MapToEntity(TDto dto);
        /// <summary>
        /// Updates an existing entity with data from the provided update DTO.
        /// </summary>
        /// <param name="dto">The DTO containing updated data.</param>
        /// <param name="entity">The existing entity to update.</param>
        /// <returns>The updated entity.</returns>
        public abstract TEntity MapDtoToEntity(TUpdateDto dto, TEntity entity);



    }
}
