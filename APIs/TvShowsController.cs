using System;
using System.Collections.Generic;
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
    public class TvShowsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public TvShowsController(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: api/TvShows
        [HttpGet]
        [Authorize(AuthenticationSchemes ="Api",Roles ="Admin,User")]
        public async Task<ActionResult<IEnumerable<TvShowDto>>> GetTvShows()
        {
            var tvShows = await _context.TvShows.ProjectTo<TvShowDto>(_mapper.ConfigurationProvider).ToListAsync();
            return tvShows;
        }

        // GET: api/TvShows/5
        [HttpGet("{id}")]
        [Authorize(AuthenticationSchemes = "Api",Roles ="Admin,User")]
        public async Task<ActionResult<TvShowDto>> GetTvShow(Guid id)
        {
            var tvShow = await _context.TvShows.ProjectTo<TvShowDto>(_mapper.ConfigurationProvider).FirstOrDefaultAsync(x => x.TvShowId == id);

            if (tvShow == null)
            {
                return NotFound();
            }

            return tvShow;
        }

        // PUT: api/TvShows/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        [Authorize(AuthenticationSchemes = "Api", Roles = "Admin")]
        public async Task<IActionResult> PutTvShow(Guid id, TvShow tvShow)
        {
            if (id != tvShow.TvShowId)
            {
                return BadRequest();
            }

            _context.Entry(tvShow).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TvShowExists(id))
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

        // POST: api/TvShows
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [Authorize(AuthenticationSchemes = "Api", Roles = "Admin")]
        public async Task<ActionResult<TvShow>> PostTvShow(TvShow tvShow)
        {
            _context.TvShows.Add(tvShow);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTvShow", new { id = tvShow.TvShowId }, tvShow);
        }

        // DELETE: api/TvShows/5
        [HttpDelete("{id}")]
        [Authorize(AuthenticationSchemes = "Api", Roles = "Admin")]
        public async Task<IActionResult> DeleteTvShow(Guid id)
        {
            var tvShow = await _context.TvShows.FindAsync(id);
            if (tvShow == null)
            {
                return NotFound();
            }

            _context.TvShows.Remove(tvShow);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TvShowExists(Guid id)
        {
            return _context.TvShows.Any(e => e.TvShowId == id);
        }
    }
}
