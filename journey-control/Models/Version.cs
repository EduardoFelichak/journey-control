using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace journey_control.Models
{
    [Table("versions")]
    public class Version
    {
        [Key]
        public int Id { get; set; }
        [Key]
        public int ProjectId { get; set; }
        public string Name { get; set; }
        public DateOnly StartDate { get; set; } 
        public DateOnly DueDate { get; set; }
        public Project Project { get; set; }

        public ICollection<Task> Tasks { get; set; }
    }
}
