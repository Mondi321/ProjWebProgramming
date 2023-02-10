namespace ProjWebProgramming.Models
{
    public class Actor
    {
        public Guid ActorId { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Nationality { get; set; } = string.Empty;
        public DateTime BirthDate { get; set; }
        public byte[]? Image { get; set; }
        public ICollection<Movie> Movies { get; set; } = new List<Movie>();
        public List<MovieActors>? MovieActors { get; set; }
        public ICollection<TvShow> TvShows { get; set; } = new List<TvShow>();
        public List<TvShowActor>? TvShowActors { get; set; }
    }
}
