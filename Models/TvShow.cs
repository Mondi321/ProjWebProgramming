namespace ProjWebProgramming.Models
{
    public class TvShow
    {
        public Guid TvShowId { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public DateTime ReleaseYear { get; set; }
        public decimal Rating { get; set; }
        public int Seasons { get; set; }
        public int Episodes { get; set; }
        public decimal EpisodeLength { get; set; }
        public byte[]? Image { get; set; }
        public byte[]? ImageCarousel { get; set; }
        public ICollection<Genre> Genres { get; set; } = new List<Genre>();
        public List<TvShowGenre>? TvShowGenres { get; set; }
        public ICollection<Actor> Actors { get; set; } = new List<Actor>();
        public List<TvShowActor>? TvShowActors { get; set; }
        public Guid DirectorId { get; set; }
        public Director? Director { get; set; }
        public ICollection<User> Users { get; set; } = new List<User>();
        public List<TvShowWishlist>? TvShowWishlists { get; set; }
    }
}
