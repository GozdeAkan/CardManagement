
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
        }
    }
}