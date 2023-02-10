using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using ProjWebProgramming.Data;
using ProjWebProgramming.Models;

namespace ProjWebProgramming.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class TvShowGenresController : Controller
    {
        private readonly ApplicationDbContext _context;

        public TvShowGenresController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: TvShowGenres
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



            var tvShowGenres = from g in _context.TvShowGenres.Include(t => t.Genre).Include(t => t.TvShow)
                               select g;

            if (!String.IsNullOrEmpty(searchString))
            {
                tvShowGenres = tvShowGenres.Where(s => s.TvShow.Title.Contains(searchString));
            }

            switch (sortOrder)
            {
                case "name_desc":
                    tvShowGenres = tvShowGenres.OrderByDescending(g => g.TvShow.Title);
                    break;
                default:
                    tvShowGenres = tvShowGenres.OrderBy(g => g.TvShow.Title);
                    break;
            }
            int pageSize = 3;

            return View(await PaginatedList<TvShowGenre>.CreateAsync(tvShowGenres.AsNoTracking(), pageNumber ?? 1, pageSize));

        }

        // GET: TvShowGenres/Details/5
        //public async Task<IActionResult> Details(Guid? id)
        //{
        //    if (id == null || _context.TvShowGenres == null)
        //    {
        //        return NotFound();
        //    }

        //    var tvShowGenre = await _context.TvShowGenres
        //        .Include(t => t.Genre)
        //        .Include(t => t.TvShow)
        //        .FirstOrDefaultAsync(m => m.TvShowId == id);
        //    if (tvShowGenre == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(tvShowGenre);
        //}

        // GET: TvShowGenres/Create
        public IActionResult Create()
        {
            ViewData["Genre"] = _context.Genres;
            ViewData["TvShowId"] = new SelectList(_context.TvShows, "TvShowId", "Title");
            return View();
        }

        // POST: TvShowGenres/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Guid tvShowId, Guid[] genreIds)
        {
            foreach(Guid genreId in genreIds)
            {
                TvShowGenre tvShowGenre = new TvShowGenre();
                tvShowGenre.TvShowId = tvShowId;
                tvShowGenre.GenreId = genreId;
                this._context.TvShowGenres.Add(tvShowGenre);
                this._context.SaveChanges();
            }
            return RedirectToAction(nameof(Index));
        }

        // GET: TvShowGenres/Edit/5
        //public async Task<IActionResult> Edit(Guid? id)
        //{
        //    if (id == null || _context.TvShowGenres == null)
        //    {
        //        return NotFound();
        //    }

        //    var tvShowGenre = await _context.TvShowGenres.FindAsync(id);
        //    if (tvShowGenre == null)
        //    {
        //        return NotFound();
        //    }
        //    ViewData["GenreId"] = new SelectList(_context.Genres, "GenreId", "GenreId", tvShowGenre.GenreId);
        //    ViewData["TvShowId"] = new SelectList(_context.TvShows, "TvShowId", "TvShowId", tvShowGenre.TvShowId);
        //    return View(tvShowGenre);
        //}

        //// POST: TvShowGenres/Edit/5
        //// To protect from overposting attacks, enable the specific properties you want to bind to.
        //// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Edit(Guid id, [Bind("CreatedAt,TvShowId,GenreId")] TvShowGenre tvShowGenre)
        //{
        //    if (id != tvShowGenre.TvShowId)
        //    {
        //        return NotFound();
        //    }

        //    if (ModelState.IsValid)
        //    {
        //        try
        //        {
        //            _context.Update(tvShowGenre);
        //            await _context.SaveChangesAsync();
        //        }
        //        catch (DbUpdateConcurrencyException)
        //        {
        //            if (!TvShowGenreExists(tvShowGenre.TvShowId))
        //            {
        //                return NotFound();
        //            }
        //            else
        //            {
        //                throw;
        //            }
        //        }
        //        return RedirectToAction(nameof(Index));
        //    }
        //    ViewData["GenreId"] = new SelectList(_context.Genres, "GenreId", "GenreId", tvShowGenre.GenreId);
        //    ViewData["TvShowId"] = new SelectList(_context.TvShows, "TvShowId", "TvShowId", tvShowGenre.TvShowId);
        //    return View(tvShowGenre);
        //}

        // GET: TvShowGenres/Delete/5
        public async Task<IActionResult> Delete(Guid? tvShowId, Guid? genreId)
        {
            if (tvShowId == null || genreId == null || _context.TvShowGenres == null)
            {
                return NotFound();
            }

            var tvShowGenre = await _context.TvShowGenres
                .Include(t => t.Genre)
                .Include(t => t.TvShow)
                .FirstOrDefaultAsync(m => m.TvShowId == tvShowId && m.GenreId == genreId);
            if (tvShowGenre == null)
            {
                return NotFound();
            }

            return View(tvShowGenre);
        }

        // POST: TvShowGenres/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid tvShowId, Guid genreId)
        {
            if (_context.TvShowGenres == null)
            {
                return Problem("Entity set 'ApplicationDbContext.TvShowGenres'  is null.");
            }
            var tvShowGenre = await _context.TvShowGenres.FindAsync(tvShowId, genreId);
            if (tvShowGenre != null)
            {
                _context.TvShowGenres.Remove(tvShowGenre);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        //private bool TvShowGenreExists(Guid id)
        //{
        //  return _context.TvShowGenres.Any(e => e.TvShowId == id);
        //}
    }
}
