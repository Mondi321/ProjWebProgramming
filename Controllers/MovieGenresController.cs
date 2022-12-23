using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ProjWebProgramming.Data;
using ProjWebProgramming.Models;

namespace ProjWebProgramming.Controllers
{
    public class MovieGenresController : Controller
    {
        private readonly ApplicationDbContext _context;

        public MovieGenresController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: MovieGenres
        public async Task<IActionResult> Index(
             string sortOrder,
             string currentFilter,
             string searchString,
             int? pageNumber)
        {
            ViewData["CurrentSort"] = sortOrder;
            ViewData["NameSortParm"] = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewData["DateSortParm"] = sortOrder == "Date" ? "date_desc" : "Date";

            if (searchString != null)
            {
                pageNumber = 1;
            }
            else
            {

                searchString = currentFilter;

            }

            ViewData["CurrentFilter"] = searchString;

            var moviesGenre = from mg in _context.MovieGenre
                         select mg;


            switch (sortOrder)
            {
                case "name_desc":
                    moviesGenre = moviesGenre.OrderByDescending(mg => mg.Movie);
                    break;
                case "Date":
                    moviesGenre = moviesGenre.OrderBy(mg => mg.Genre);
                    break;
            }
            int pageSize = 3;

            return View(await PaginatedList<MovieGenre>.CreateAsync(moviesGenre.AsNoTracking(), pageNumber ?? 1, pageSize));
        }

        // GET: MovieGenres/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null || _context.MovieGenre == null)
            {
                return NotFound();
            }

            var movieGenre = await _context.MovieGenre
                .Include(m => m.Genre)
                .Include(m => m.Movie)
                .FirstOrDefaultAsync(m => m.MovieId == id);
            if (movieGenre == null)
            {
                return NotFound();
            }

            return View(movieGenre);
        }

        // GET: MovieGenres/Create
        public IActionResult Create()
        {
            ViewData["Genre"] = this._context.Genres;
            ViewData["MovieId"] = new SelectList(_context.Movies, "MovieId", "Title");
            return View();
        }

        [HttpPost]
        public IActionResult Create(Guid movieId, Guid[] genreIds)
        {
            foreach(Guid genreId in genreIds)
            {
                MovieGenre movieGenre = new MovieGenre();
                movieGenre.MovieId = movieId;
                movieGenre.GenreId = genreId;
                this._context.MovieGenre.Add(movieGenre);
                this._context.SaveChanges();
            }
            return RedirectToAction(nameof(Index));
        }

        // POST: MovieGenres/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Create([Bind("CreatedAt,MovieId,GenreId")] MovieGenre movieGenre)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        movieGenre.MovieId = Guid.NewGuid();
        //        _context.Add(movieGenre);
        //        await _context.SaveChangesAsync();
        //        return RedirectToAction(nameof(Index));
        //    }
        //    ViewData["GenreId"] = new SelectList(_context.Genres, "GenreId", "Name", movieGenre.GenreId);
        //    ViewData["MovieId"] = new SelectList(_context.Movies, "MovieId", "Title", movieGenre.MovieId);
        //    return View(movieGenre);
        //}

        // GET: MovieGenres/Edit/5
        public async Task<IActionResult> Edit(Guid movieId, Guid genreId)
        {
            if (_context.MovieGenre == null)
            {
                return NotFound();
            }

            var movieGenre = await _context.MovieGenre.FindAsync(movieId,genreId);
            if (movieGenre == null)
            {
                return NotFound();
            }
            ViewData["GenreId"] = new SelectList(_context.Genres, "GenreId", "Name", movieGenre.GenreId);
            ViewData["MovieId"] = new SelectList(_context.Movies, "MovieId", "Title", movieGenre.MovieId);
            return View(movieGenre);
        }

        // POST: MovieGenres/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Edit([Bind("CreatedAt,MovieId,GenreId")] MovieGenre movieGenre)
        //{
        //    if (movieGenre == null)
        //    {
        //        return NotFound();
        //    }

        //    if (ModelState.IsValid)
        //    {
        //        try
        //        {
        //            MovieGenre movieGenre1 = new MovieGenre();
        //            movieGenre1.MovieId = movieGenre.MovieId;
        //            movieGenre1.GenreId = movieGenre.GenreId;
        //            movieGenre1.CreatedAt = movieGenre.CreatedAt;
        //            _context.Update(movieGenre1);
        //            await _context.SaveChangesAsync();
        //        }
        //        catch (DbUpdateConcurrencyException)
        //        {
        //            if (!MovieGenreExists(movieGenre.MovieId))
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
        //    ViewData["GenreId"] = new SelectList(_context.Genres, "GenreId", "Name", movieGenre.GenreId);
        //    ViewData["MovieId"] = new SelectList(_context.Movies, "MovieId", "Title", movieGenre.MovieId);
        //    return View(movieGenre);
        //}

        // GET: MovieGenres/Delete/5
        //public async Task<IActionResult> Delete(Guid? id)
        //{
        //    if (id == null || _context.MovieGenre == null)
        //    {
        //        return NotFound();
        //    }

        //    var movieGenre = await _context.MovieGenre
        //        .Include(m => m.Genre)
        //        .Include(m => m.Movie)
        //        .FirstOrDefaultAsync(m => m.MovieId == id);
        //    if (movieGenre == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(movieGenre);
        //}

        // POST: MovieGenres/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> DeleteConfirmed(Guid id)
        //{
        //    if (_context.MovieGenre == null)
        //    {
        //        return Problem("Entity set 'ApplicationDbContext.MovieGenre'  is null.");
        //    }
        //    var movieGenre = await _context.MovieGenre.FindAsync(id);
        //    if (movieGenre != null)
        //    {
        //        _context.MovieGenre.Remove(movieGenre);
        //    }
            
        //    await _context.SaveChangesAsync();
        //    return RedirectToAction(nameof(Index));
        //}

        //private bool MovieGenreExists(Guid id)
        //{
        //  return _context.MovieGenre.Any(e => e.MovieId == id);
        //}
    }
}
