using ProjWebProgramming.AutoMapper.Profiles;

namespace ProjWebProgramming.Models.DTOs
{
    public class GenreDto
    {
        public Guid GenreId { get; set; }
        public string Name { get; set; } = string.Empty;
        public List<MovieProfile> Movies { get; set; }
        public List<TvShowProfile> TvShows { get; set; }
    }
}
