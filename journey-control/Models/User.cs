namespace journey_control.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Login { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime LastLoginOn { get; set; }
        public string ApiKey { get; set; }
        public int ProjectId { get; set; }
    }
}
