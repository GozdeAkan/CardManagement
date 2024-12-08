using Business.Contracts;
using Business.DTOs.Card;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers
{
    /// <summary>
    /// Handles CRUD operations for Cards.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class CardController : ControllerBase
    {
        private readonly ICardService _cardService;
        /// <summary>
        /// Initializes a new instance of the <see cref="CardController"/> class.
        /// </summary>
        /// <param name="cardService">The card service to handle business logic.</param>

        public CardController(ICardService cardService)
        {
            _cardService = cardService;
        }

        /// <summary>
        /// Retrieves all cards.
        /// </summary>
        /// <returns>A list of all cards.</returns>
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var cards = await _cardService.GetAllAsync();
            return Ok(cards);
        }

        /// <summary>
        /// Retrieves a specific card by its ID.
        /// </summary>
        /// <param name="id">The unique identifier of the card.</param>
        /// <returns>The card with the specified ID, or a not found result if it does not exist.</returns>

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var card = await _cardService.GetByIdAsync(id);
            if (card == null)
                throw new Exception($"Card with ID {id} not found.");
            return Ok(card);
        }
        /// <summary>
        /// Creates a new card.
        /// </summary>
        /// <param name="cardDto">The data transfer object containing the card details.</param>
        /// <returns>An OK result if the card is created successfully.</returns>
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CardDto cardDto)
        {
            await _cardService.CreateAsync(cardDto);
            return Ok();
        }

        /// <summary>
        /// Updates an existing card by its ID.
        /// </summary>
        /// <param name="id">The unique identifier of the card to be updated.</param>
        /// <param name="cardDto">The data transfer object containing the updated card details.</param>
        /// <returns>An OK result if the card is updated successfully.</returns>
        [HttpPut("{id:guid}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] CardUpdateDto cardDto)
        {
            await _cardService.UpdateAsync(id, cardDto);
            return Ok();
        }

    }
}
