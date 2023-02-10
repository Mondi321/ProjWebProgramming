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
    public class TvShowsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public TvShowsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: TvShows
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.TvShows.Include(t => t.Director).Include(t => t.Genres).Include(t => t.Actors);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: TvShows/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null || _context.TvShows == null)
            {
                return NotFound();
            }

            var tvShow = await _context.TvShows
                .Include(t => t.Director)
                .Include(m => m.Genres)
                .Include(m => m.Actors)
                .FirstOrDefaultAsync(m => m.TvShowId == id);
            if (tvShow == null)
            {
                return NotFound();
            }

            return View(tvShow);
        }

        // GET: TvShows/Create
        public IActionResult Create()
        {
            ViewData["DirectorId"] = new SelectList(_context.Directors, "DirectorId", "FirstName");
            return View();
        }

        // POST: TvShows/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("TvShowId,Title,Description,ReleaseYear,Rating,Seasons,Episodes,EpisodeLength,Image,ImageCarousel,DirectorId,Type")] TvShow tvShow)
        {
            if (ModelState.IsValid)
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
                        tvShow.Image = pic;
                    }
                    using (var fileStreamCarousel = files[1].OpenReadStream())
                    {
                        using (var memoryStreamCarousel = new MemoryStream())
                        {
                            fileStreamCarousel.CopyTo(memoryStreamCarousel);
                            picCarousel = memoryStreamCarousel.ToArray();
                        }
                        tvShow.ImageCarousel = picCarousel;
                    }
                }
                tvShow.TvShowId = Guid.NewGuid();
                _context.Add(tvShow);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["DirectorId"] = new SelectList(_context.Directors, "DirectorId", "FirstName", tvShow.DirectorId);
            return View(tvShow);
        }

        // GET: TvShows/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null || _context.TvShows == null)
            {
                return NotFound();
            }

            var tvShow = await _context.TvShows.FindAsync(id);
            if (tvShow == null)
            {
                return NotFound();
            }
            ViewData["DirectorId"] = new SelectList(_context.Directors, "DirectorId", "FirstName", tvShow.DirectorId);
            return View(tvShow);
        }

        // POST: TvShows/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("TvShowId,Title,Description,ReleaseYear,Rating,Seasons,Episodes,EpisodeLength,Image,ImageCarousel,DirectorId")] TvShow tvShow)
        {
            if (id != tvShow.TvShowId)
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
                            tvShow.Image = pic;
                        }
                        using (var fileStreamCarousel = files[1].OpenReadStream())
                        {
                            using (var memoryStreamCarousel = new MemoryStream())
                            {
                                fileStreamCarousel.CopyTo(memoryStreamCarousel);
                                picCarousel = memoryStreamCarousel.ToArray();
                            }
                            tvShow.ImageCarousel = picCarousel;
                        }
                    }
                    _context.Update(tvShow);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TvShowExists(tvShow.TvShowId))
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
            ViewData["DirectorId"] = new SelectList(_context.Directors, "DirectorId", "FirstName", tvShow.DirectorId);
            return View(tvShow);
        }

        // GET: TvShows/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null || _context.TvShows == null)
            {
                return NotFound();
            }

            var tvShow = await _context.TvShows
                .Include(t => t.Director)
                .FirstOrDefaultAsync(m => m.TvShowId == id);
            if (tvShow == null)
            {
                return NotFound();
            }

            return View(tvShow);
        }

        // POST: TvShows/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            if (_context.TvShows == null)
            {
                return Problem("Entity set 'ApplicationDbContext.TvShows'  is null.");
            }
            var tvShow = await _context.TvShows.FindAsync(id);
            if (tvShow != null)
            {
                _context.TvShows.Remove(tvShow);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TvShowExists(Guid id)
        {
          return _context.TvShows.Any(e => e.TvShowId == id);
        }
    }
}
