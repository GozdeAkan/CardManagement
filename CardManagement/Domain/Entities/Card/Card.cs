
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using Domain.Entities.Base;
using Domain.Enum;

namespace Domain.Entities.Card
{
    public class Card : BaseEntity
    {
        [Required]
        public string CardName { get; set; }
        [JsonIgnore]
        public Guid CardTypeId { get; set; } 
        public string? ImageUrl { get; set; } 
        [Required]
        public string Description { get; set; } 
        public Status Status { get; set; } = Status.NotStarted;
        [Required]
        public  CardType CardType { get; set; }
        [Required]
        public ICollection<CardQuestion> Questions { get; set; } 
    
    
    }
}
