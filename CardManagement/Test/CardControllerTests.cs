

using Business.Contracts;
using Business.DTOs;
using Business.DTOs.Card;
using Domain.Entities.Card;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Moq;
using Presentation.Controllers;

namespace Test
{
    public class CardControllerTests
    {
        private readonly Mock<ICardService> _mockCardService;
        private readonly CardController _cardController;

        public CardControllerTests()
        {
            _mockCardService = new Mock<ICardService>();
            _cardController = new CardController(_mockCardService.Object);
        }
        [Fact]
        public async Task GetAll_ShouldReturnOkWithCardList()
        {
            // Arrange
            var cards = new List<Card>
            {
                new Card { CardName = "Card 1", Description = "Description 1" },
                new Card { CardName = "Card 2", Description = "Description 2" }
            };
            _mockCardService.Setup(service => service.GetAllAsync(null, null, null))
                .ReturnsAsync(cards);

            // Act
            var result = await _cardController.GetAll();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnedCards = Assert.IsAssignableFrom<IEnumerable<Card>>(okResult.Value);
            Assert.Equal(cards.Count(), returnedCards.Count());
        }
        [Fact]
        public async Task GetAll_ShouldReturnOkWithDetailedCardList()
        {
            // Arrange
            var cards = new List<Card>
            {
                new Card { CardName = "Card 1", Description = "Description 1", Questions = new List<CardQuestion> { new CardQuestion {Text = "Card Question 1" } } },
                new Card { CardName = "Card 2", Description = "Description 2" }
            };
            _mockCardService.Setup(service => service.GetAllAsync(null, null, null))
                .ReturnsAsync(cards);

            // Act
            var result = await _cardController.GetAll();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnedCards = Assert.IsAssignableFrom<IEnumerable<Card>>(okResult.Value);
            Assert.Equal(cards.Count(), returnedCards.Count());
        }

        [Fact]
        public async Task GetById_ShouldReturnOkWithCard()
        {
            // Arrange
            var cardId = Guid.NewGuid();
            var card = new Card { Id = cardId, CardName = "Card 1", Description = "Description 1" };

            _mockCardService.Setup(service => service.GetByIdAsync(cardId, null))
                .ReturnsAsync(card);

            // Act
            var result = await _cardController.GetById(cardId);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnedCard = Assert.IsType<Card>(okResult.Value);
            Assert.Equal(cardId, returnedCard.Id);
        }

        [Fact]
        public async Task GetById_ShouldReturnNotFound_WhenCardDoesNotExist()
        {
            // Arrange
            var cardId = Guid.NewGuid();

            _mockCardService.Setup(service => service.GetByIdAsync(cardId, null))
                .ReturnsAsync((Card)null);

            // Act
            var result = await _cardController.GetById(cardId);

            // Assert
            Assert.IsType<NotFoundObjectResult>(result);
        }


        [Fact]
        public async Task Create_ShouldReturnOk_WhenCardIsCreated()
        {
            // Arrange
            var cardDto = new CardDto { CardName = "New Card", Description = "New Description" };

            _mockCardService.Setup(service => service.CreateAsync(cardDto))
                .Returns(Task.CompletedTask);

            // Act
            var result = await _cardController.Create(cardDto);

            // Assert
            Assert.IsType<OkResult>(result);
            _mockCardService.Verify(service => service.CreateAsync(cardDto), Times.Once);
        }

        [Fact]
        public async Task Update_ShouldReturnOk_WhenCardIsUpdated()
        {
            // Arrange
            var cardId = Guid.NewGuid();
            var updateDto = new CardUpdateDto { CardName = "Updated Card", Description = "Updated Description" };

            _mockCardService.Setup(service => service.UpdateAsync(cardId, updateDto, null))
                .Returns(Task.CompletedTask);

            // Act
            var result = await _cardController.Update(cardId, updateDto);

            // Assert
            Assert.IsType<OkResult>(result);
            _mockCardService.Verify(service => service.UpdateAsync(cardId, updateDto, null), Times.Once);
        }
    }
}
