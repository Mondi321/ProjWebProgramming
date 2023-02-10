namespace ProjWebProgramming.Models
{
    public class TvShowGenre
    {
        public DateTime CreatedAt { get; set; }

        public Guid TvShowId { get; set; }
        public TvShow? TvShow { get; set; }

        public Guid GenreId { get; set; }
        public Genre? Genre { get; set; }
    }
}
