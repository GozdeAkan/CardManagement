using Domain.Enum;

namespace Business.DTOs.Card
{
    /// <summary>
    /// Represents a data transfer object (DTO) for updating a card.
    /// </summary>
    public class CardUpdateDto
    {
        /// <summary>
        /// Gets or sets the updated name of the card.
        /// </summary>
        /// <remarks>
        /// This is the new name or title that will replace the existing name of the card.
        /// </remarks>
        public string CardName { get; set; }

        /// <summary>
        /// Gets or sets the identifier for the type of the card.
        /// </summary>
        /// <remarks>
        /// This is used to specify or update the type of the card, represented by a unique identifier.
        /// </remarks>
        public Guid CardTypeId { get; set; }

        /// <summary>
        /// Gets or sets the updated URL of the card's image.
        /// </summary>
        /// <remarks>
        /// This property stores the link to the updated image representing the card.
        /// </remarks>
        public string ImageUrl { get; set; }

        /// <summary>
        /// Gets or sets the updated description of the card.
        /// </summary>
        /// <remarks>
        /// This property provides additional details or information about the card.
        /// </remarks>
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the updated status of the card.
        /// </summary>
        /// <remarks>
        /// The status is represented by the <see cref="Status"/> enum, which can include values
        /// such as NotStarted or Done.
        /// </remarks>
        public Status Status { get; set; }

        /// <summary>
        /// Gets or sets the list of updated questions associated with the card.
        /// </summary>
        /// <remarks>
        /// Each question is represented as a <see cref="CardQuestionUpdateDto"/>, allowing 
        /// multiple questions to be updated along with the card.
        /// </remarks>
        public List<CardQuestionUpdateDto> Questions { get; set; }
    }
}
