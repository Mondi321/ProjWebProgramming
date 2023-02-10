using ProjWebProgramming.AutoMapper.Profiles;

namespace ProjWebProgramming.Models.DTOs
{
    public class MovieGenreDto
    {
        public DateTime CreatedAt { get; set; }

        public Guid MovieId { get; set; }
        public MovieProfile Movie { get; set; }

        public Guid GenreId { get; set; }
        public GenreProfile Genre { get; set; }
    }
}
