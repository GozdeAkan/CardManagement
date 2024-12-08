using Business.Contracts;
using Business.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CardController : ControllerBase
    {
        private readonly ICardService _cardService;

        public CardController(ICardService cardService)
        {
            _cardService = cardService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var cards = await _cardService.GetAllAsync();
            return Ok(cards);
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var card = await _cardService.GetByIdAsync(id);
            if (card == null)
                return NotFound($"Card with ID {id} not found.");
            return Ok(card);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CardDto cardDto)
        {
            await _cardService.CreateAsync(cardDto);
            return Ok();
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] CardUpdateDto cardDto)
        {
            await _cardService.UpdateAsync(id, cardDto);
            return Ok();
        }

    }
}
