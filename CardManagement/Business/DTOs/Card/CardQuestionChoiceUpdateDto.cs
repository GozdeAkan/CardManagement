namespace Business.DTOs.Card
{
    /// <summary>
    /// Represents the data transfer object (DTO) used for updating a choice in a card question.
    /// </summary>
    public class CardQuestionChoiceUpdateDto
    {
        /// <summary>
        /// Gets or sets the unique identifier of the choice.
        /// </summary>
        /// <remarks>
        /// This identifier is used to locate and update the specific choice in the database.
        /// </remarks>
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the updated text of the choice.
        /// </summary>
        /// <remarks>
        /// This is the new label or content that will replace the existing text of the choice.
        /// </remarks>
        public string Text { get; set; }

        /// <summary>
        /// Gets or sets the updated order of the choice relative to other choices.
        /// </summary>
        public int SortIndex { get; set; }
    }
}
