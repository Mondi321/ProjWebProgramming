using Microsoft.AspNetCore.Mvc;

namespace ProjWebProgramming.Models
{
    [NonViewComponent]
    public class Review
    {
        public Guid Id { get; set; }
        public string Mesazhi { get; set; } = string.Empty;
        public int RatingValue { get; set; }
        public Guid UserId { get; set; }
        public User? User { get; set; }
    }
}
