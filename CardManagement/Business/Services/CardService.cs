
using System.Linq.Expressions;
using AutoMapper;
using Business.Contracts;
using Business.DTOs.Card;
using DataAccess.Utils;
using Domain.Entities.Card;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;

namespace Business.Services
{
    /// <summary>
    /// Service implementation for managing card entities and their related operations.
    /// </summary>
    public class CardService : BaseService<Card, CardDto, CardUpdateDto>, ICardService
    {
        private readonly IMapper _mapper;

        /// <summary>
        /// Initializes a new instance of the <see cref="CardService"/> class.
        /// </summary>
        /// <param name="unitOfWork">The unit of work for managing transactions and repositories.</param>
        /// <param name="mapper">The AutoMapper instance for mapping DTOs and entities.</param>
        public CardService(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork)
        {
            _mapper = mapper;
        }
        /// <summary>
        /// Retrieves a list of all cards with their related questions, choices, and card types.
        /// </summary>
        /// <returns>A task that represents the asynchronous operation. The task result contains the list of cards.</returns>

        public Task<List<Card>> GetAllCardAsync()
        {
            return base.GetAllAsync(null, null, i => i.Include(inc => inc.Questions).ThenInclude(inc => inc.Choices).Include(inc => inc.CardType));
        }
        /// <summary>
        /// Retrieves a card by its unique identifier, including related questions, choices, and card type.
        /// </summary>
        /// <param name="id">The unique identifier of the card.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the card.</returns>
        public Task<Card> GetCardByIdAsync(Guid id)
        {
            return base.GetByIdAsync(id, i => i.Include(inc => inc.Questions).ThenInclude(inc => inc.Choices).Include(inc => inc.CardType));
        }

        /// <summary>
        /// Updates an existing card and its related entities using the provided DTO.
        /// </summary>
        /// <param name="id">The unique identifier of the card to update.</param>
        /// <param name="dto">The data transfer object containing updated card information.</param>
        /// <returns>A task that represents the asynchronous operation.</returns>

        public Task UpdateCardAsync(Guid id, CardUpdateDto dto)
        {
            return base.UpdateAsync(id, dto, i => i.Include(inc => inc.Questions).ThenInclude(inc => inc.Choices).Include(inc => inc.CardType));
        }

        /// <summary>
        /// Maps a <see cref="CardDto"/> to a <see cref="Card"/> entity.
        /// </summary>
        /// <param name="dto">The data transfer object to map.</param>
        /// <returns>The mapped card entity.</returns>
        public override Card MapToEntity(CardDto dto)
        {
            return _mapper.Map<Card>(dto);
        }

        /// <summary>
        /// Updates an existing <see cref="Card"/> entity using the data from a <see cref="CardUpdateDto"/>.
        /// </summary>
        /// <param name="dto">The data transfer object containing updated information.</param>
        /// <param name="entity">The existing card entity to update.</param>
        /// <returns>The updated card entity.</returns>
        public override Card MapDtoToEntity(CardUpdateDto dto, Card entity)
        {
           _mapper.Map(dto, entity);
           return entity;
        }

      

      
       
    }
}