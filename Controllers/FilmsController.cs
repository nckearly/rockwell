using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Rockwell.Models;

using Microsoft.AspNetCore.Authorization;

namespace Rockwell.Controllers
{
    public class FilmsController : Controller
    {
        private readonly RWStudiosContext _context;

        public FilmsController(RWStudiosContext context)
        {
            _context = context;
        }

        // GET: Films
        [Authorize(Roles ="Admin")]
        public async Task<IActionResult> Index()
        {
            var rWStudiosContext = _context.Film.Include(f => f.RatingFkNavigation);
            return View(await rWStudiosContext.ToListAsync());
        }

        // GET: Films/Details/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var film = await _context.Film
                .Include(f => f.RatingFkNavigation)
                .FirstOrDefaultAsync(m => m.FilmPk == id);
            if (film == null)
            {
                return NotFound();
            }

            return View(film);
        }

        // GET: Films/Create
        [Authorize(Roles = "Admin")]
        public IActionResult Create()
        {
            ViewData["RatingFk"] = new SelectList(_context.FilmRating, "RatingPk", "Rating");
            return View();
        }

        // POST: Films/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create([Bind("MovieTitle,PitchText,AmountBudgeted,RatingFk,Summary,DateInTheaters")] Film film)
        {
            if (ModelState.IsValid)
            {
                _context.Add(film);
                await _context.SaveChangesAsync();
                TempData["message"] = $"{film.MovieTitle} added successfully";
                return RedirectToAction(nameof(Index));
            }
            ViewData["RatingFk"] = new SelectList(_context.FilmRating, "RatingPk", "Rating", film.RatingFk);
            return View(film);
        }

        // GET: Films/Edit/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var film = await _context.Film.FindAsync(id);
            if (film == null)
            {
                return NotFound();
            }
            ViewData["RatingFk"] = new SelectList(_context.FilmRating, "RatingPk", "Rating", film.RatingFk);
            return View(film);
        }
        // GET: Films/Edit/5
        
        public async Task<IActionResult> IndexPublic(string filmtitle, int? rating, int? maxbudget)
        {
            var films = from somefilms in _context.Film select somefilms;


            if (!String.IsNullOrEmpty(filmtitle))
            {
                films = films.Where(f => f.MovieTitle.Contains(filmtitle));
            }

            if (rating.HasValue)
            {
                films = films.Where(f => f.RatingFk == rating);
            }
            if (maxbudget.HasValue)
            {
                films = films.Where(f => f.AmountBudgeted <= maxbudget);
            }
            ViewData["filmtitle"] = filmtitle;
            ViewData["maxbudget"] = maxbudget;
            ViewData["RatingFK"] = new SelectList(_context.FilmRating, "RatingPk", "Rating", rating);
            return View(await films.Include(f => f.RatingFkNavigation).OrderBy(f => f.MovieTitle).ToListAsync());
        }

        // POST: Films/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("FilmPk,MovieTitle,PitchText,AmountBudgeted,RatingFk,Summary,ImageName,DateInTheaters")] Film film)
        {
            if (id != film.FilmPk)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(film);
                    TempData["message"] = $"{film.MovieTitle} updated successfully";
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FilmExists(film.FilmPk))
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
            ViewData["RatingFk"] = new SelectList(_context.FilmRating, "RatingPk", "Rating", film.RatingFk);
            return View(film);
        }

        // GET: Films/Delete/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var film = await _context.Film
                .Include(f => f.RatingFkNavigation)
                .FirstOrDefaultAsync(m => m.FilmPk == id);
            if (film == null)
            {
                return NotFound();
            }

            return View(film);
        }

        // POST: Films/Delete/5
        [Authorize(Roles = "Admin")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var film = await _context.Film.FindAsync(id);
            _context.Film.Remove(film);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FilmExists(int id)
        {
            return _context.Film.Any(e => e.FilmPk == id);
        }
    }
}
