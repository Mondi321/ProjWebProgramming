namespace ProjWebProgramming.AutoMapper.Profiles
{
    public class TvShowProfile
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
        public Guid DirectorId { get; set; }
    }
}
