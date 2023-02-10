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
    public class TvShowActorsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public TvShowActorsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: TvShowActors
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



            var tvShowActors = from g in _context.TvShowActors.Include(t => t.Actor).Include(t => t.TvShow)
                               select g;

            if (!String.IsNullOrEmpty(searchString))
            {
                tvShowActors = tvShowActors.Where(s => s.TvShow.Title.Contains(searchString));
            }

            switch (sortOrder)
            {
                case "name_desc":
                    tvShowActors = tvShowActors.OrderByDescending(g => g.TvShow.Title);
                    break;
                default:
                    tvShowActors = tvShowActors.OrderBy(g => g.TvShow.Title);
                    break;
            }
            int pageSize = 3;

            return View(await PaginatedList<TvShowActor>.CreateAsync(tvShowActors.AsNoTracking(), pageNumber ?? 1, pageSize));

        }

        // GET: TvShowActors/Details/5
        //public async Task<IActionResult> Details(Guid? id)
        //{
        //    if (id == null || _context.TvShowActors == null)
        //    {
        //        return NotFound();
        //    }

        //    var tvShowActor = await _context.TvShowActors
        //        .Include(t => t.Actor)
        //        .Include(t => t.TvShow)
        //        .FirstOrDefaultAsync(m => m.ActorId == id);
        //    if (tvShowActor == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(tvShowActor);
        //}

        // GET: TvShowActors/Create
        public IActionResult Create()
        {
            ViewData["Actor"] = _context.Actor;
            ViewData["TvShowId"] = new SelectList(_context.TvShows, "TvShowId", "Title");
            return View();
        }

        // POST: TvShowActors/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Guid tvShowId, Guid[] actorIds)
        {
            foreach (Guid actorId in actorIds)
            {
                TvShowActor tvShowActors = new TvShowActor();
                tvShowActors.TvShowId = tvShowId;
                tvShowActors.ActorId = actorId;
                this._context.TvShowActors.Add(tvShowActors);
                this._context.SaveChanges();
            }
            return RedirectToAction(nameof(Index));
        }

        // GET: TvShowActors/Edit/5
        //public async Task<IActionResult> Edit(Guid? id)
        //{
        //    if (id == null || _context.TvShowActors == null)
        //    {
        //        return NotFound();
        //    }

        //    var tvShowActor = await _context.TvShowActors.FindAsync(id);
        //    if (tvShowActor == null)
        //    {
        //        return NotFound();
        //    }
        //    ViewData["ActorId"] = new SelectList(_context.Actor, "ActorId", "ActorId", tvShowActor.ActorId);
        //    ViewData["TvShowId"] = new SelectList(_context.TvShows, "TvShowId", "TvShowId", tvShowActor.TvShowId);
        //    return View(tvShowActor);
        //}

        // POST: TvShowActors/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Edit(Guid id, [Bind("TvShowId,ActorId")] TvShowActor tvShowActor)
        //{
        //    if (id != tvShowActor.ActorId)
        //    {
        //        return NotFound();
        //    }

        //    if (ModelState.IsValid)
        //    {
        //        try
        //        {
        //            _context.Update(tvShowActor);
        //            await _context.SaveChangesAsync();
        //        }
        //        catch (DbUpdateConcurrencyException)
        //        {
        //            if (!TvShowActorExists(tvShowActor.ActorId))
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
        //    ViewData["ActorId"] = new SelectList(_context.Actor, "ActorId", "ActorId", tvShowActor.ActorId);
        //    ViewData["TvShowId"] = new SelectList(_context.TvShows, "TvShowId", "TvShowId", tvShowActor.TvShowId);
        //    return View(tvShowActor);
        //}

        // GET: TvShowActors/Delete/5
        public async Task<IActionResult> Delete(Guid? tvShowId, Guid? actorId)
        {
            if (tvShowId == null || actorId == null || _context.TvShowActors == null)
            {
                return NotFound();
            }

            var tvShowActor = await _context.TvShowActors
                .Include(t => t.Actor)
                .Include(t => t.TvShow)
                .FirstOrDefaultAsync(m => m.TvShowId == tvShowId && m.ActorId == actorId);
            if (tvShowActor == null)
            {
                return NotFound();
            }

            return View(tvShowActor);
        }

        // POST: TvShowActors/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid tvShowId, Guid actorId)
        {
            if (_context.TvShowActors == null)
            {
                return Problem("Entity set 'ApplicationDbContext.TvShowActors'  is null.");
            }
            var tvShowActor = await _context.TvShowActors.FindAsync(tvShowId, actorId);
            if (tvShowActor != null)
            {
                _context.TvShowActors.Remove(tvShowActor);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        //private bool TvShowActorExists(Guid id)
        //{
        //  return _context.TvShowActors.Any(e => e.ActorId == id);
        //}
    }
}
