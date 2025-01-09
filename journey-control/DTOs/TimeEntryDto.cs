using System.Text.Json.Serialization;

namespace journey_control.DTOs
{
    public class TimeEntryDto
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("hours")]
        public double Hours { get; set; }

        [JsonPropertyName("spent_on")]
        public string SpentOn { get; set; }

        [JsonPropertyName("activity")]
        public ActivityDto Activity { get; set; }

        [JsonIgnore]
        public int ActivityId { get; set; }

        [JsonIgnore]
        public int? ProjectId { get; set; }

        [JsonPropertyName("issue")]
        public IssueDto Issue { get; set; }

        [JsonIgnore]
        public int? IssueId { get; set; }

        [JsonPropertyName("comments")]
        public string Comments { get; set; }

        [JsonPropertyName("custom_fields")]
        public List<CustomFieldDto> CustomFields { get; set; } = new();
    }

}
