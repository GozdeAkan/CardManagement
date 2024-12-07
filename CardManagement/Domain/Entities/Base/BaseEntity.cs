

namespace Domain.Entities.Base
{
    public class BaseEntity
    {
        public Guid Id { get; set; } 
        public DateTime CreatedTime { get; set; } = DateTime.UtcNow; //TODO: try onbeforesaving or tracking
        public DateTime? UpdatedTime { get; set; } 
        public required string CreatedBy { get; set; } 
        public string? UpdatedBy { get; set; } 
    }
}
