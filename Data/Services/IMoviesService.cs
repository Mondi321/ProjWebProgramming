using ProjWebProgramming.Models;

namespace ProjWebProgramming.Data.Services
{
    public interface IMoviesService
    {
        Task<Movie> GetMovieByIdAsync(Guid id);
    }
}
