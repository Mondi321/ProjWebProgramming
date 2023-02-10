using ProjWebProgramming.AutoMapper.Profiles;

namespace ProjWebProgramming.Models.DTOs
{
    public class ReviewDto
    {
        public Guid Id { get; set; }
        public string Mesazhi { get; set; } = string.Empty;
        public int RatingValue { get; set; }
        public Guid UserId { get; set; }
        public UserProfile User { get; set; }
    }
}
