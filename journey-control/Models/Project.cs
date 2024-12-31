using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace journey_control.Models
{
    [Table("projects")]
    public class Project
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }

        public ICollection<Version> Versions { get; set; }
    }
}
