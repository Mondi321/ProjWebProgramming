using Microsoft.EntityFrameworkCore;
using ProjWebProgramming.Models;
using System;

namespace ProjWebProgramming.Data.Services
{
    public class MoviesService:IMoviesService
    {
        private readonly ApplicationDbContext _context;
        public MoviesService(ApplicationDbContext context) 
        {
            _context = context;
        }

        public async Task<Movie> GetMovieByIdAsync(Guid id)
        {
            var movieDetails = await _context.Movies
                .Include(p => p.Director)
                .Include(p => p.Genres)
                .Include(p => p.Actors)
                .FirstOrDefaultAsync(n => n.MovieId == id);

            return movieDetails;
        }
    }
}
