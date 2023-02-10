using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ProjWebProgramming.Data;
using ProjWebProgramming.Models;

namespace ProjWebProgramming.Controllers
{
    public class MoviesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public MoviesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Movies
        [Authorize(Roles ="User,Admin")]
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Movies.Include(m => m.Director).Include(m => m.Genres).Include(m => m.Actors);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Movies/Details/5
        [Authorize(Roles = "User,Admin")]
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null || _context.Movies == null)
            {
                return NotFound();
            }

            var movie = await _context.Movies
                .Include(m => m.Director)
                .Include(m => m.Genres)
                .Include(m => m.Actors)
                .FirstOrDefaultAsync(m => m.MovieId == id);
            if (movie == null)
            {
                return NotFound();
            }

            return View(movie);
        }

        [Authorize(Roles = "User,Admin")]
        public async Task<IActionResult> Filter(string searchString)
        {
            var allMovies = await _context.Movies.Include(m => m.Director).Include(m => m.Genres).Include(m => m.Actors).ToListAsync();

            if (!string.IsNullOrEmpty(searchString))
            {
                var filteredResult = allMovies.Where(n => n.Title.ToLower().Contains(searchString.ToLower()) || n.Description.ToLower().Contains(searchString.ToLower())).ToList();

                //var filteredResultNew = allMovies.Where(n => string.Equals(n.Title, searchString, StringComparison.CurrentCultureIgnoreCase) || string.Equals(n.Description, searchString, StringComparison.CurrentCultureIgnoreCase)).ToList();

                return View("Index", filteredResult);
            }

            return View("Index", allMovies);
        }

        // GET: Movies/Create
        [Authorize(Roles ="Admin")]
        public IActionResult Create()
        {
            ViewData["DirectorId"] = new SelectList(_context.Directors, "DirectorId", "FirstName");
            return View();
        }

        // POST: Movies/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MovieId,Title,Description,ReleaseYear,Rating,MovieLength,Price,DirectorId,Image,ImageCarousel")] Movie movie)
        {
            if (ModelState.IsValid)
            {
                var files = HttpContext.Request.Form.Files;
                if(files.Count() > 0)
                {
                    byte[] pic = null;
                    byte[] picCarousel = null;
                    using(var fileStream = files[0].OpenReadStream())
                    {
                        using(var memoryStream = new MemoryStream())
                        {
                            fileStream.CopyTo(memoryStream);
                            pic = memoryStream.ToArray();
                        }
                        movie.Image = pic;
                    }
                    using (var fileStreamCarousel = files[1].OpenReadStream())
                    {
                        using (var memoryStreamCarousel = new MemoryStream())
                        {
                            fileStreamCarousel.CopyTo(memoryStreamCarousel);
                            picCarousel = memoryStreamCarousel.ToArray();
                        }
                        movie.ImageCarousel = picCarousel;
                    }
                }
                movie.MovieId = Guid.NewGuid();
                _context.Add(movie);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["DirectorId"] = new SelectList(_context.Directors, "DirectorId", "FirstName", movie.DirectorId);
            return View(movie);
        }

        // GET: Movies/Edit/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null || _context.Movies == null)
            {
                return NotFound();
            }

            var movie = await _context.Movies.FindAsync(id);
            if (movie == null)
            {
                return NotFound();
            }
            ViewData["DirectorId"] = new SelectList(_context.Directors, "DirectorId", "FirstName", movie.DirectorId);
            return View(movie);
        }

        // POST: Movies/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("MovieId,Title,Description,ReleaseYear,Rating,MovieLength,Price,DirectorId,Image,ImageCarousel")] Movie movie)
        {
            if (id != movie.MovieId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var files = HttpContext.Request.Form.Files;
                    if (files.Count() > 0)
                    {
                        byte[] pic = null;
                        byte[] picCarousel = null;
                        using (var fileStream = files[0].OpenReadStream())
                        {
                            using (var memoryStream = new MemoryStream())
                            {
                                fileStream.CopyTo(memoryStream);
                                pic = memoryStream.ToArray();
                            }
                            movie.Image = pic;
                        }
                        using (var fileStreamCarousel = files[1].OpenReadStream())
                        {
                            using (var memoryStreamCarousel = new MemoryStream())
                            {
                                fileStreamCarousel.CopyTo(memoryStreamCarousel);
                                picCarousel = memoryStreamCarousel.ToArray();
                            }
                            movie.ImageCarousel = picCarousel;
                        }
                    }
                    _context.Update(movie);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MovieExists(movie.MovieId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["DirectorId"] = new SelectList(_context.Directors, "DirectorId", "FirstName", movie.DirectorId);
            return View(movie);
        }

        // GET: Movies/Delete/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null || _context.Movies == null)
            {
                return NotFound();
            }

            var movie = await _context.Movies
                .Include(m => m.Director)
                .FirstOrDefaultAsync(m => m.MovieId == id);
            if (movie == null)
            {
                return NotFound();
            }

            return View(movie);
        }

        // POST: Movies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            if (_context.Movies == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Movies'  is null.");
            }
            var movie = await _context.Movies.FindAsync(id);
            if (movie != null)
            {
                _context.Movies.Remove(movie);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MovieExists(Guid id)
        {
          return _context.Movies.Any(e => e.MovieId == id);
        }
    }
}
