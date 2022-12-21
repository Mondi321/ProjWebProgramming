namespace ProjWebProgramming.Models
{
    public class Movie
    {
        public Guid MovieId { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public DateTime ReleaseYear { get; set; }
        public decimal Rating { get; set; }
        public decimal MovieLength { get; set; }
        public ICollection<Genre> Genres { get; set; } = new List<Genre>();
        public ICollection<User>? Users { get; set; } = new List<User>();
    }
}
