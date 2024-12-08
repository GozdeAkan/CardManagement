
using System.Text.Json.Serialization;
using Domain.Entities.Base;

namespace Domain.Entities.Card
{
    public class CardQuestion : BaseEntity
    {
        [JsonIgnore]
        public Guid CardId { get; set; }
        public string Text { get; set; } 
        public int SortIndex { get; set; }
        [JsonIgnore]
        public Card Card { get; set; } 
        public ICollection<CardQuestionChoice> Choices { get; set; } 
    }
}