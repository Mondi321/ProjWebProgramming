namespace ProjWebProgramming.Models
{
    public class Genre
    {
        public Guid GenreId { get; set; }
        public string Name { get; set; } = string.Empty;
        public ICollection<Movie> Movies { get; set; } = new List<Movie>();
        public List<MovieGenre>? MovieGenres { get; set; }
    }
}
