namespace Business.DTOs.Card
{
    /// <summary>
    /// Represents a data transfer object (DTO) for a question within a card.
    /// </summary>
    public class CardQuestionDto
    {
        /// <summary>
        /// Gets or sets the text of the question.
        /// </summary>
        /// <remarks>
        /// This is the content or label of the question that will be displayed to users.
        /// </remarks>
        public string Text { get; set; }

        /// <summary>
        /// Gets or sets the order of the question relative to other questions.
        /// </summary>
        public int SortIndex { get; set; }

        /// <summary>
        /// Gets or sets the list of choices associated with the question.
        /// </summary>
        /// <remarks>
        /// Each question can have multiple choices represented as a collection of 
        /// <see cref="CardQuestionChoiceDto"/>.
        /// </remarks>
        public List<CardQuestionChoiceDto> Choices { get; set; }
    }
}
