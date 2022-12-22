using Microsoft.Extensions.Hosting;

namespace ProjWebProgramming.Models
{
    public class MovieGenre
    {
        public DateTime CreatedAt { get; set; }

        public Guid MovieId { get; set; }
        public Movie? Movie { get; set; }

        public Guid GenreId { get; set; }
        public Genre? Genre { get; set; }
    }
}
