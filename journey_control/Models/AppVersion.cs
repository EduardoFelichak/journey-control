using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace journey_control.Models
{
    [Table("app_version")]
    public class AppVersion
    {
        [Key]
        public Guid Id { get; set; } = new Guid();
        public string Name { get; set; }
    }
}
