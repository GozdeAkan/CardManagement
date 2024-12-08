using System;

namespace Business.DTOs
{
    /// <summary>
    /// Represents a choice for a question within a card.
    /// </summary>
    public class CardQuestionChoiceDto
    {
        /// <summary>
        /// Gets or sets the text of the choice.
        /// </summary>
        /// <remarks>
        /// This is the label or content displayed for the choice in the question.
        /// </remarks>
        public string Text { get; set; }

        /// <summary>
        /// Gets or sets the order of the choice relative to other choices.
        /// </summary>
        public int SortIndex { get; set; }
    }
}
