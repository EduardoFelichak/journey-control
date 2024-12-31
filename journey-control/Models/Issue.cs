namespace journey_control.Models
{
    public class Issue
    {
        public int Id { get; set; }
        public string Subject { get; set; }
        public string Description { get; set; }
        public int ProjectId { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? DueDate { get; set; }
        public string Size { get; set; }
        public int FixedVersion { get; set; }
        public string Status { get; set; }
    }
}
