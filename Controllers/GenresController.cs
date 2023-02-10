using System;
using System.Collections.Generic;
using System.Data;
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
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class GenresController : Controller
    {
        private readonly ApplicationDbContext _context;

        public GenresController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Genres
        public async Task<IActionResult> Index(
              string sortOrder,
              string currentFilter,
              string searchString,
              int? pageNumber)
        {
            ViewData["CurrentSort"] = sortOrder;
            ViewData["NameSortParm"] = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewData["CurrentFilter"] = searchString;



            if (searchString != null)
            {
                pageNumber = 1;
            }
            else
            {

                searchString = currentFilter;

            }



            var genres = from g in _context.Genres
                              select g;

            if (!String.IsNullOrEmpty(searchString))
            {
                genres = genres.Where(s => s.Name.Contains(searchString));
            }

            switch (sortOrder)
            {
                case "name_desc":
                    genres = genres.OrderByDescending(g => g.Name);
                    break;
                default:
                    genres = genres.OrderBy(g => g.Name);
                    break;
            }
            int pageSize = 3;

            return View(await PaginatedList<Genre>.CreateAsync(genres.AsNoTracking(), pageNumber ?? 1, pageSize));
        }

        // GET: Genres/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null || _context.Genres == null)
            {
                return NotFound();
            }

            var genre = await _context.Genres
                .FirstOrDefaultAsync(m => m.GenreId == id);
            if (genre == null)
            {
                return NotFound();
            }

            return View(genre);
        }

        // GET: Genres/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Genres/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("GenreId,Name")] Genre genre)
        {
            if (ModelState.IsValid)
            {
                genre.GenreId = Guid.NewGuid();
                _context.Add(genre);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(genre);
        }

        // GET: Genres/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null || _context.Genres == null)
            {
                return NotFound();
            }

            var genre = await _context.Genres.FindAsync(id);
            if (genre == null)
            {
                return NotFound();
            }
            return View(genre);
        }

        // POST: Genres/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("GenreId,Name")] Genre genre)
        {
            if (id != genre.GenreId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(genre);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!GenreExists(genre.GenreId))
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
            return View(genre);
        }

        // GET: Genres/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null || _context.Genres == null)
            {
                return NotFound();
            }

            var genre = await _context.Genres
                .FirstOrDefaultAsync(m => m.GenreId == id);
            if (genre == null)
            {
                return NotFound();
            }

            return View(genre);
        }

        // POST: Genres/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            if (_context.Genres == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Genres'  is null.");
            }
            var genre = await _context.Genres.FindAsync(id);
            if (genre != null)
            {
                _context.Genres.Remove(genre);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool GenreExists(Guid id)
        {
          return _context.Genres.Any(e => e.GenreId == id);
        }
    }
}
