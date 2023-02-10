namespace ProjWebProgramming.Models
{
    public class TvShowActor
    {
        public Guid TvShowId { get; set; }
        public TvShow? TvShow { get; set; }

        public Guid ActorId { get; set; }
        public Actor? Actor { get; set; }
    }
}
