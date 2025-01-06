using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace journey_control.Models
{
    [Table("entries")]
    public class Entrie
    {
        [Key]
        public int Id { get; set; }
        public string TaskId { get; set; }
        public int TaskUserId { get; set; }
        public Task Task { get; set; }
        public DateOnly DateEntrie { get; set; }
        public int Duration { get; set; }
        public bool IsInProgress { get; set; }
    }
}
