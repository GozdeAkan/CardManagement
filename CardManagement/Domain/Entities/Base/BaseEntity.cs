

using System.Text.Json.Serialization;

namespace Domain.Entities.Base
{
    public class BaseEntity
    {
        public Guid Id { get; set; }
        [JsonIgnore]
        public DateTime CreatedTime { get; set; } = DateTime.UtcNow; //TODO: try onbeforesaving or tracking
        [JsonIgnore]
        public DateTime? UpdatedTime { get; set; }
        [JsonIgnore]
        public string CreatedBy { get; set; }
        [JsonIgnore]
        public string? UpdatedBy { get; set; } 
    }
}
