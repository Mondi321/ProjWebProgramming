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
    public class MovieActorsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public MovieActorsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: MovieActors
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.MovieActors.Include(m => m.Actor).Include(m => m.Movie);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: MovieActors/Details/5
        //public async Task<IActionResult> Details(Guid? id)
        //{
        //    if (id == null || _context.MovieActors == null)
        //    {
        //        return NotFound();
        //    }

        //    var movieActors = await _context.MovieActors
        //        .Include(m => m.Actor)
        //        .Include(m => m.Movie)
        //        .FirstOrDefaultAsync(m => m.ActorId == id);
        //    if (movieActors == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(movieActors);
        //}

        // GET: MovieActors/Create
        public IActionResult Create()
        {
            ViewData["Actor"] = this._context.Actor;
            ViewData["MovieId"] = new SelectList(_context.Movies, "MovieId", "Title");
            return View();
        }

        [HttpPost]
        public IActionResult Create(Guid movieId, Guid[] actorIds)
        {
            foreach (Guid actorId in actorIds)
            {
                MovieActors movieActors= new MovieActors();
                movieActors.MovieId = movieId;
                movieActors.ActorId = actorId;
                this._context.MovieActors.Add(movieActors);
                this._context.SaveChanges();
            }
            return RedirectToAction(nameof(Index));
        }

        // GET: MovieActors/Edit/5
        //public async Task<IActionResult> Edit(Guid? id)
        //{
        //    if (id == null || _context.MovieActors == null)
        //    {
        //        return NotFound();
        //    }

        //    var movieActors = await _context.MovieActors.FindAsync(id);
        //    if (movieActors == null)
        //    {
        //        return NotFound();
        //    }
        //    ViewData["ActorId"] = new SelectList(_context.Actor, "ActorId", "ActorId", movieActors.ActorId);
        //    ViewData["MovieId"] = new SelectList(_context.Movies, "MovieId", "MovieId", movieActors.MovieId);
        //    return View(movieActors);
        //}

        // POST: MovieActors/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Edit(Guid id, [Bind("MovieId,ActorId")] MovieActors movieActors)
        //{
        //    if (id != movieActors.ActorId)
        //    {
        //        return NotFound();
        //    }

        //    if (ModelState.IsValid)
        //    {
        //        try
        //        {
        //            _context.Update(movieActors);
        //            await _context.SaveChangesAsync();
        //        }
        //        catch (DbUpdateConcurrencyException)
        //        {
        //            if (!MovieActorsExists(movieActors.ActorId))
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
        //    ViewData["ActorId"] = new SelectList(_context.Actor, "ActorId", "ActorId", movieActors.ActorId);
        //    ViewData["MovieId"] = new SelectList(_context.Movies, "MovieId", "MovieId", movieActors.MovieId);
        //    return View(movieActors);
        //}

        // GET: MovieActors/Delete/5
        public async Task<IActionResult> Delete(Guid movieId, Guid actorId)
        {
            if (movieId == null || actorId == null || _context.MovieActors == null)
            {
                return NotFound();
            }

            var movieActors = await _context.MovieActors
                .Include(m => m.Actor)
                .Include(m => m.Movie)
                .FirstOrDefaultAsync(m => m.MovieId == movieId && m.ActorId == actorId);
            if (movieActors == null)
            {
                return NotFound();
            }

            return View(movieActors);
        }

        // POST: MovieActors/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid movieId, Guid actorId)
        {
            if (_context.MovieActors == null)
            {
                return Problem("Entity set 'ApplicationDbContext.MovieActors'  is null.");
            }
            var movieActors = await _context.MovieActors.FindAsync(movieId, actorId);
            if (movieActors != null)
            {
                _context.MovieActors.Remove(movieActors);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        //private bool MovieActorsExists(Guid id)
        //{
        //  return _context.MovieActors.Any(e => e.ActorId == id);
        //}
    }
}
