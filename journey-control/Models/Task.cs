using journey_control.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace journey_control.Models
{
    [Table("tasks")]
    public class Task
    {
        [Key]
        public string Id { get; set; }
        [Key]
        public int UserId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public SizeE Size { get; set; }
        public string Status { get; set; }
        public DateOnly StartDate { get; set; }
        public DateOnly DueDate { get; set; }
        public int Project { get; set; }
        public int VersionId { get; set; }
        public int VersionProjectId { get; set; }
        public Version Version { get; set; }

        public ICollection<Entrie> Entries { get; set; }
        public ICollection<LocalEntrie> LocalEntries { get; set; }  

        public Task() { }        
    }
}
