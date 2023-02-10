using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjWebProgramming.Data;
using ProjWebProgramming.Models;
using ProjWebProgramming.Models.DTOs;

namespace ProjWebProgramming.APIs
{
    [Route("api/[controller]")]
    [ApiController]
    public class MoviesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public MoviesController(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: api/Movies
        [HttpGet]
        [Authorize(AuthenticationSchemes ="Api",Roles ="Admin,User")]
        public async Task<ActionResult<IEnumerable<MovieDto>>> GetMovies()
        {
            var movies = await _context.Movies.ProjectTo<MovieDto>(_mapper.ConfigurationProvider).ToListAsync();
            return movies;
        }

        [HttpGet]
        [Route("Toprated")]
        [Authorize(AuthenticationSchemes = "Api", Roles = "Admin,User")]
        public async Task<ActionResult<IEnumerable<MovieDto>>> GetMoviesTopRated()
        {
            var movies = await _context.Movies.ProjectTo<MovieDto>(_mapper.ConfigurationProvider).Where(m => (Double)m.Rating >= 8.5).ToListAsync();
            return movies;
        }

        [HttpGet("bygenre/{id}")]
        [Authorize(AuthenticationSchemes = "Api",Roles ="Admin,User")]
        public async Task<ActionResult<IEnumerable<MovieGenreDto>>> GetMoviesByGenre(Guid id)
        {
            var movies = await _context.MovieGenre.ProjectTo<MovieGenreDto>(_mapper.ConfigurationProvider).Where(m => m.GenreId == id).ToListAsync();
            return movies;
        }


        // GET: api/Movies/5
        [HttpGet("{id}")]
        [Authorize(AuthenticationSchemes = "Api", Roles = "Admin,User")]
        public async Task<ActionResult<MovieDto>> GetMovie(Guid id)
        {
            var movie = await _context.Movies.ProjectTo<MovieDto>(_mapper.ConfigurationProvider).FirstOrDefaultAsync(x => x.MovieId == id);

            if (movie == null)
            {
                return NotFound();
            }

            return movie;
        }

        // PUT: api/Movies/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        [Authorize(AuthenticationSchemes = "Api", Roles = "Admin")]
        public async Task<IActionResult> PutMovie(Guid id, Movie movie)
        {
            if (id != movie.MovieId)
            {
                return BadRequest();
            }

            _context.Entry(movie).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MovieExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Movies
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [Authorize(AuthenticationSchemes = "Api", Roles = "Admin")]
        public async Task<ActionResult<Movie>> PostMovie(Movie movie)
        {
            _context.Movies.Add(movie);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetMovie", new { id = movie.MovieId }, movie);
        }

        // DELETE: api/Movies/5
        [HttpDelete("{id}")]
        [Authorize(AuthenticationSchemes = "Api", Roles = "Admin")]
        public async Task<IActionResult> DeleteMovie(Guid id)
        {
            var movie = await _context.Movies.FindAsync(id);
            if (movie == null)
            {
                return NotFound();
            }

            _context.Movies.Remove(movie);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool MovieExists(Guid id)
        {
            return _context.Movies.Any(e => e.MovieId == id);
        }
    }
}
