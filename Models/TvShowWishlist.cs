namespace ProjWebProgramming.Models
{
    public class TvShowWishlist
    {
        public DateTime CreatedAt { get; set; }
        public Guid UserId { get; set; }
        public User? User { get; set; }
        public Guid TvShowId { get; set; }
        public TvShow? TvShow { get; set; }
    }
}
