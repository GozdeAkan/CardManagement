using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess.Context;
using DataAccess.Repositories;
using Domain.Entities.Card;
using Microsoft.EntityFrameworkCore;

namespace Test
{
    public class CardRepositoryTests
    {
        private readonly DbContextOptions<AppDbContext> _dbContextOptions;

        public CardRepositoryTests()
        {
            _dbContextOptions = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: "TestCardDatabase")
                .Options;
        }

        [Fact]
        public async Task GetByIdAsync_ShouldReturnCard_WhenCardExists()
        {
            // Arrange
            var cardId = Guid.NewGuid();
            var card = new Card { Id = cardId, CardName = "Test Card", Description = "Test Description", CreatedBy = "System" };

            using (var context = new AppDbContext(_dbContextOptions))
            {
                context.Cards.Add(card);
                await context.SaveChangesAsync();
            }

            // Act
            Card result;
            using (var context = new AppDbContext(_dbContextOptions))
            {
                var repository = new CardRepository(context);
                result = await repository.GetByIdAsync(cardId);
            }

            // Assert
            Assert.NotNull(result);
            Assert.Equal(cardId, result.Id);
            Assert.Equal("Test Card", result.CardName);
        }

        [Fact]
        public async Task GetByIdAsync_ShouldReturnNull_WhenCardDoesNotExist()
        {
            // Arrange
            var cardId = Guid.NewGuid();

            // Act
            Card result;
            using (var context = new AppDbContext(_dbContextOptions))
            {
                var repository = new CardRepository(context);
                result = await repository.GetByIdAsync(cardId);
            }

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task AddAsync_ShouldAddNewCard()
        {
            // Arrange
            var card = new Card { Id = Guid.NewGuid(), CardName = "New Card", Description = "New Description", CreatedBy = "System" };

            using (var context = new AppDbContext(_dbContextOptions))
            {
                var repository = new CardRepository(context);

                // Act
                await repository.AddAsync(card);
                await context.SaveChangesAsync();
            }

            // Assert
            using (var context = new AppDbContext(_dbContextOptions))
            {
                var savedCard = await context.Cards.FirstOrDefaultAsync(c => c.Id == card.Id);
                Assert.NotNull(savedCard);
                Assert.Equal("New Card", savedCard.CardName);
            }
        }

        [Fact]
        public async Task UpdateAsync_ShouldUpdateExistingCard()
        {
            // Arrange
            var card = new Card { Id = Guid.NewGuid(), CardName = "Old Card", Description = "Old Description", CreatedBy = "System" };

            using (var context = new AppDbContext(_dbContextOptions))
            {
                context.Cards.Add(card);
                await context.SaveChangesAsync();
            }

            // Act
            using (var context = new AppDbContext(_dbContextOptions))
            {
                var repository = new CardRepository(context);
                card.CardName = "Updated Card";
                card.Description = "Updated Description";
                card.CreatedBy = "System";
                await repository.UpdateAsync(card);
                await context.SaveChangesAsync();
            }

            // Assert
            using (var context = new AppDbContext(_dbContextOptions))
            {
                var updatedCard = await context.Cards.FirstOrDefaultAsync(c => c.Id == card.Id);
                Assert.NotNull(updatedCard);
                Assert.Equal("Updated Card", updatedCard.CardName);
                Assert.Equal("Updated Description", updatedCard.Description);
            }
        }

    }
}
