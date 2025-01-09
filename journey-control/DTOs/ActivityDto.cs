using System.Text.Json.Serialization;

namespace journey_control.DTOs
{
    public class ActivityDto
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }
        [JsonPropertyName("name")]
        public string Name { get; set; }
    }
}
