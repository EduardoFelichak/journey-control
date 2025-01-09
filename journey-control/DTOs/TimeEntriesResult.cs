using System.Text.Json.Serialization;

namespace journey_control.DTOs
{
    public class TimeEntriesResult
    {
        [JsonPropertyName("time_entries")]
        public List<TimeEntryDto> TimeEntries { get; set; }
    }
}
