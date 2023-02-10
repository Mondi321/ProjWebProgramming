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
        public double Price { get; set; }
        public byte[]? Image { get; set; }
        public byte[]? ImageCarousel { get; set; }
        public ICollection<Genre> Genres { get; set; } = new List<Genre>();
        public List<MovieGenre>? MovieGenres { get; set; }
        public ICollection<Actor> Actors{ get; set; } = new List<Actor>();
        public List<MovieActors>? MovieActors{ get; set; }
        public ICollection<User> Users { get; set; } = new List<User>();
        public List<Wishlist>? Wishlists { get; set; }
        public Guid DirectorId { get; set; }
        public Director? Director { get; set; }
    }
}
