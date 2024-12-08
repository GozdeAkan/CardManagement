using Business.DTOs.Card;
using Domain.Entities.Card;

namespace Business.Contracts
{
    /// <summary>
    /// Defines the service contract for operations related to cards.
    /// </summary>
    /// <remarks>
    /// This interface extends the <see cref="IBaseService{TEntity, TDto, TUpdateDto}"/> 
    /// to provide specific CRUD operations for the <see cref="Card"/> entity.
    /// </remarks>
    public interface ICardService : IBaseService<Card, CardDto, CardUpdateDto>
    {
    }
}
