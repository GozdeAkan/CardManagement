using System;
using System.Collections.Generic;

namespace Business.DTOs.Card
{
    /// <summary>
    /// Represents a data transfer object (DTO) for updating a question within a card.
    /// </summary>
    public class CardQuestionUpdateDto
    {
        /// <summary>
        /// Gets or sets the unique identifier of the question.
        /// </summary>
        /// <remarks>
        /// This ID is used to identify and update the specific question in the database.
        /// </remarks>
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the updated text of the question.
        /// </summary>
        /// <remarks>
        /// This is the new content or label of the question to replace the existing one.
        /// </remarks>
        public string Text { get; set; }

        /// <summary>
        /// Gets or sets the updated order of the question relative to other questions.
        /// </summary>
        public int SortIndex { get; set; }

        /// <summary>
        /// Gets or sets the list of updated choices associated with the question.
        /// </summary>
        /// <remarks>
        /// Each question can have multiple updated choices represented as a collection of 
        /// <see cref="CardQuestionChoiceUpdateDto"/>.
        /// </remarks>
        public List<CardQuestionChoiceUpdateDto> Choices { get; set; }
    }
}
