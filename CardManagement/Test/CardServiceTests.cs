

using System.Linq.Expressions;
using AutoMapper;
using Business.DTOs.Card;
using Business.Services;
using DataAccess.Repositories.Base;
using DataAccess.Utils;
using Domain.Entities.Card;
using Infrastructure.Utils;
using Microsoft.EntityFrameworkCore.Query;
using Moq;

namespace Test
{
    public class CardServiceTests
    {
        private readonly Mock<IUnitOfWork> _mockUnitOfWork;
        private readonly Mock<IMapper> _mockMapper;
        private readonly Mock<IBaseRepository<Card>> _mockCardRepository;
        private readonly CardService _cardService;

        public CardServiceTests()
        {
            _mockUnitOfWork = new Mock<IUnitOfWork>();
            _mockMapper = new Mock<IMapper>();
            _mockCardRepository = new Mock<IBaseRepository<Card>>();

            _mockUnitOfWork.Setup(uow => uow.GetRepository<Card>())
                .Returns(_mockCardRepository.Object);

            _cardService = new CardService(_mockUnitOfWork.Object, _mockMapper.Object);
        }

        [Fact]
        public async Task GetAllAsync_ShouldReturnAllCardsWithDetails()
        {
            // Arrange
            var cards = new List<Card>
        {
            new Card
            {
                CardName = "Card 1",
                Description = "Description 1",
                Questions = new List<CardQuestion>
                {
                    new CardQuestion
                    {
                        Text = "Question 1",
                        Choices = new List<CardQuestionChoice>
                        {
                            new CardQuestionChoice { Text = "Choice 1" },
                            new CardQuestionChoice { Text = "Choice 2" }
                        }
                    }
                },
                CardTypeId = Guid.NewGuid(),
                CreatedBy = "System"
            },
            new Card { CardName = "Card 2", Description = "Description 2", CreatedBy = "System" }
        };

            _mockCardRepository.Setup(repo => repo.GetAllAsync(
                    It.IsAny<Expression<Func<Card, Card>>>(),
                    null,
                    It.IsAny<Func<IQueryable<Card>, IIncludableQueryable<Card, object>>>(),
                    null))
                .ReturnsAsync(cards);

            // Act
            var result = await _cardService.GetAllAsync();

            // Assert
            Assert.NotNull(result);
            Assert.Equal(cards.Count, result.Count);
            Assert.Contains(result, c => c.CardName == "Card 1");
        }

        [Fact]
        public async Task GetByIdAsync_ShouldReturnCardWithDetails()
        {
            // Arrange
            var cardId = Guid.NewGuid();
            var card = new Card
            {
                Id = cardId,
                CardName = "Card 1",
                Description = "Description 1",
                Questions = new List<CardQuestion>
            {
                new CardQuestion
                {
                    Text = "Question 1",
                    Choices = new List<CardQuestionChoice>
                    {
                        new CardQuestionChoice { Text = "Choice 1" }
                    }
                }
            },
                CardTypeId= Guid.NewGuid()
            };

            _mockCardRepository.Setup(repo => repo.GetByIdAsync(cardId, It.IsAny<Func<IQueryable<Card>, IIncludableQueryable<Card, object>>>()))
                .ReturnsAsync(card);

            // Act
            var result = await _cardService.GetByIdAsync(cardId);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(cardId, result.Id);
            Assert.Equal("Card 1", result.CardName);
        }

        [Fact]
        public void MapToEntity_ShouldMapCardDtoToCard()
        {
            // Arrange
            var cardDto = new CardDto { CardName = "New Card", Description = "New Description" };
            var mappedCard = new Card { CardName = "New Card", Description = "New Description" };

            _mockMapper.Setup(mapper => mapper.Map<Card>(cardDto))
                .Returns(mappedCard);

            // Act
            var result = _cardService.MapToEntity(cardDto);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(mappedCard.CardName, result.CardName);
        }

        [Fact]
        public void MapDtoToEntity_ShouldUpdateCardFromCardUpdateDto()
        {
            // Arrange
            var updateDto = new CardUpdateDto { CardName = "Updated Card", Description = "Updated Description" };
            var card = new Card { CardName = "Old Card", Description = "Old Description" };

            _mockMapper.Setup(mapper => mapper.Map(updateDto, card))
                .Callback<CardUpdateDto, Card>((dto, entity) =>
                {
                    entity.CardName = dto.CardName;
                    entity.Description = dto.Description;
                });

            // Act
            var result = _cardService.MapDtoToEntity(updateDto, card);

            // Assert
            Assert.NotNull(result);
            Assert.Equal("Updated Card", result.CardName);
            Assert.Equal("Updated Description", result.Description);
        }
    }
}

