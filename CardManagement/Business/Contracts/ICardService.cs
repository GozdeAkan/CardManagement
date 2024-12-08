using System.Linq.Expressions;
using Business.DTOs.Card;
using Domain.Entities.Card;
using Microsoft.EntityFrameworkCore.Query;

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
        /// <summary>
        /// Retrieves all cards, optionally including related entities and applying filtering or ordering.
        /// </summary>
        /// <returns>A task that represents the asynchronous operation. The task result contains a list of cards.</returns>
        Task<List<Card>> GetAllCardAsync();

        /// <summary>
        /// Retrieves a card by its unique identifier, optionally including related navigation properties.
        /// </summary>
        /// <param name="id">The unique identifier of the card.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the card.</returns>
        Task<Card> GetCardByIdAsync(Guid id);

        /// <summary>
        /// Updates an existing card and its related entities using the provided DTO.
        /// </summary>
        /// <param name="id">The unique identifier of the card to update.</param>
        /// <returns>A task that represents the asynchronous operation.</returns>
        Task UpdateCardAsync(Guid id, CardUpdateDto dto);
    

    }
}
