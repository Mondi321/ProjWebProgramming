namespace ProjWebProgramming.Models
{
    public class Contact
    {
        public Guid ContactId { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Subject { get; set; } = string.Empty;
        public long Phone { get; set; }
        public string Message { get; set; } = string.Empty;
    }
}
