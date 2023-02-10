namespace ProjWebProgramming.AutoMapper.Profiles
{
    public class MovieProfile
    {
        public Guid MovieId { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public DateTime ReleaseYear { get; set; }
        public decimal Rating { get; set; }
        public decimal MovieLength { get; set; }
        public byte[]? Image { get; set; }
        public byte[]? ImageCarousel { get; set; }
        public Guid DirectorId { get; set; }
    }
}
