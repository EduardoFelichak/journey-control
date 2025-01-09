using System.Text.Json.Serialization;

namespace journey_control.DTOs
{
    public class IssueDto
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }
    }
}
