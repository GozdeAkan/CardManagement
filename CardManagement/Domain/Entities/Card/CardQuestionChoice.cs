
using Domain.Entities.Base;

namespace Domain.Entities.Card
{
    public class CardQuestionChoice : BaseEntity
    {
        public Guid QuestionId { get; set; }
        public string Text { get; set; } 
        public int SortIndex { get; set; } 
        public CardQuestion Question { get; set; } 
    }
}