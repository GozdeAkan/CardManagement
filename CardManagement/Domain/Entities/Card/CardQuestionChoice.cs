
using System.Text.Json.Serialization;
using Domain.Entities.Base;

namespace Domain.Entities.Card
{
    public class CardQuestionChoice : BaseEntity
    {
        [JsonIgnore]
        public Guid QuestionId { get; set; }
        public string Text { get; set; } 
        public int SortIndex { get; set; }
        [JsonIgnore]
        public CardQuestion Question { get; set; } 
    }
}