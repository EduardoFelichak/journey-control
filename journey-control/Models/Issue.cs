namespace journey_control.Models
{
    public class Issue
    {
        public int Id { get; set; }
        public string Subject { get; set; }
        public string Description { get; set; }
        public int ProjectId { get; set; }
        public DateOnly? StartDate { get; set; }
        public DateOnly? DueDate { get; set; }
        public string Size { get; set; }
        public int FixedVersion { get; set; }
        public string Status { get; set; }
    }
}
