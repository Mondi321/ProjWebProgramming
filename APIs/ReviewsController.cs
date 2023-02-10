using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjWebProgramming.Data;
using ProjWebProgramming.Models;
using ProjWebProgramming.Models.DTOs;

namespace ProjWebProgramming.APIs
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReviewsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<User> _userManager;
        private readonly IMapper _mapper;

        public ReviewsController(ApplicationDbContext context, UserManager<User> userManager, IMapper mapper)
        {
            _context = context;
            _userManager = userManager;
            _mapper = mapper;
        }

        // GET: api/Reviews
        [HttpGet]
        [Authorize(AuthenticationSchemes = "Api", Roles = "Admin,User")]
        public async Task<ActionResult<IEnumerable<ReviewDto>>> GetReview()
        {
            return await _context.Review.ProjectTo<ReviewDto>(_mapper.ConfigurationProvider).ToListAsync();
        }

        // GET: api/Reviews/5
        [HttpGet("{id}")]
        [Authorize(AuthenticationSchemes = "Api", Roles = "Admin,User")]
        public async Task<ActionResult<ReviewDto>> GetReview(Guid id)
        {
            var review = await _context.Review.ProjectTo<ReviewDto>(_mapper.ConfigurationProvider).FirstOrDefaultAsync(x => x.Id == id);

            if (review == null)
            {
                return NotFound();
            }

            return review;
        }

        // PUT: api/Reviews/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        [Authorize(AuthenticationSchemes = "Api", Roles = "Admin")]
        public async Task<IActionResult> PutReview(Guid id, Review review)
        {
            if (id != review.Id)
            {
                return BadRequest();
            }

            _context.Entry(review).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ReviewExists(id))
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

        // POST: api/Reviews
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [Authorize(AuthenticationSchemes ="Api",Roles ="Admin,User")]
        [HttpPost]
        public async Task<IActionResult> PostReview(Review review)
        {
            var user = await _userManager.FindByEmailAsync(User.FindFirstValue(ClaimTypes.Email));
            review.User = user;
            review.UserId= user.Id;
            _context.Review.Add(review);
            await _context.SaveChangesAsync();
            return Ok();
            //return CreatedAtAction("GetReview", new { id = review.Id }, review);
        }

        // DELETE: api/Reviews/5
        [HttpDelete("{id}")]
        [Authorize(AuthenticationSchemes = "Api", Roles = "Admin")]
        public async Task<IActionResult> DeleteReview(Guid id)
        {
            var review = await _context.Review.FindAsync(id);
            if (review == null)
            {
                return NotFound();
            }

            _context.Review.Remove(review);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ReviewExists(Guid id)
        {
            return _context.Review.Any(e => e.Id == id);
        }
    }
}
