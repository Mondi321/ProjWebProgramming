namespace ProjWebProgramming.Models
{
    public class Wishlist
    {
        public DateTime CreatedAt { get; set; }
        public Guid UserId { get; set; }
        public User? User { get; set; }
        public Guid MovieId { get; set; }
        public Movie? Movie { get; set; }
    }
}
