
using Domain.Entities.Auth;
using Domain.Entities.Card;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Context
{
    public class AppDbContext : IdentityDbContext<ApplicationUserEntity, IdentityRole<Guid>, Guid>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
        public DbSet<Card> Cards { get; set; }
        public DbSet<CardQuestion> CardQuestions { get; set; }
        public DbSet<CardQuestionChoice> CardQuestionChoices { get; set; }
        public DbSet<CardType> CardTypes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Card>()
                .HasOne(c => c.CardType)
                .WithMany()
                .HasForeignKey(c => c.CardTypeId);

            modelBuilder.Entity<CardQuestion>()
                .HasOne(q => q.Card)
                .WithMany(c => c.Questions)
                .HasForeignKey(q => q.CardId);

            modelBuilder.Entity<CardQuestionChoice>()
                .HasOne(c => c.Question)
                .WithMany(q => q.Choices)
                .HasForeignKey(c => c.QuestionId);

            SeedDataCardType(modelBuilder);
        }

        private void SeedDataCardType(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CardType>().HasData(
                new CardType
                {
                    Id = Guid.Parse("4e6a8a88-8c3f-45f6-9dd8-26c78a7f5172"), 
                    Name = "Air",
                    CreatedTime = DateTime.SpecifyKind(DateTime.Parse("2024-01-01T00:00:00"), DateTimeKind.Utc),
                    CreatedBy = "SeedData"
                },
                new CardType
                {
                    Id = Guid.Parse("3b3b2476-fc8a-4f17-b9b6-5d3ec1ae3f4a"), 
                    Name = "Water",
                    CreatedTime = DateTime.SpecifyKind(DateTime.Parse("2024-01-01T00:00:00"), DateTimeKind.Utc),
                    CreatedBy = "SeedData"
                }
            );
        }
    }
}