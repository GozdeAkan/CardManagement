
using Domain.Enum;

namespace Business.DTOs
{
    /// <summary>
    /// Represents the data transfer object (DTO) for a card entity.
    /// </summary>
    public class CardDto
    {
        /// <summary>
        /// Gets or sets the name of the card.
        /// </summary>
        public string CardName { get; set; }

        /// <summary>
        /// Gets or sets the identifier for the card type.
        /// </summary>
        /// <remarks>
        /// This is a foreign key or reference to the type of the card.
        /// </remarks>
        public Guid CardTypeId { get; set; }

        /// <summary>
        /// Gets or sets the URL of the card's image.
        /// </summary>
        /// <remarks>
        /// This is used to display an image representing the card.
        /// </remarks>
        public string ImageUrl { get; set; }

        /// <summary>
        /// Gets or sets the description of the card.
        /// </summary>
        /// <remarks>
        /// This provides additional details or context about the card.
        /// </remarks>
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the status of the card.
        /// </summary>
        /// <remarks>
        /// The status is represented by the <see cref="Status"/> enum, 
        /// which may include values like Active, Inactive, or Deleted.
        /// </remarks>
        public Status Status { get; set; }

        /// <summary>
        /// Gets or sets the list of questions associated with the card.
        /// </summary>
        /// <remarks>
        /// Each card can have multiple related questions represented as 
        /// a collection of <see cref="CardQuestionDto"/>.
        /// </remarks>
        public List<CardQuestionDto> Questions { get; set; }
    }
}
