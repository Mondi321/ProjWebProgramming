using ProjWebProgramming.AutoMapper.Profiles;

namespace ProjWebProgramming.Models.DTOs
{
    public class MovieDto
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
        public List<GenreProfile> Genres { get; set; }

        public List<ActorProfile> Actors { get; set; }

        public List<UserProfile> Users { get; set; }
        public Guid DirectorId { get; set; }
        public DirectorProfile Director { get; set; }

    }
}
