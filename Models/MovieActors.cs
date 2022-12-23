namespace ProjWebProgramming.Models
{
    public class MovieActors
    {
        public Guid MovieId { get; set; }
        public Movie? Movie { get; set; }

        public Guid ActorId { get; set; }
        public Actor? Actor { get; set; }
    }
}
