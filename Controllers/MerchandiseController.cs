using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

using Rockwell.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Rockwell.Controllers
{
    public class MerchandiseController : Controller
    {
        private readonly RWStudiosContext aRWContext;

        public MerchandiseController(RWStudiosContext myRWContext)
        {
            aRWContext = myRWContext;
        }

        public IActionResult TableView()
        {
            var products = aRWContext.Merchandise.Include(aMerch => aMerch.FilmFkNavigation);
            return View(products);
        }
        public IActionResult Add()
        {
            ViewData["FilmFk"] = new SelectList(aRWContext.Film.OrderBy(f=>f.MovieTitle), "FilmPk","MovieTitle");

            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Add(Merchandise merch)
        {
            if (ModelState.IsValid)
            {
            aRWContext.Add(merch);
            await aRWContext.SaveChangesAsync();
            return RedirectToAction("TableView");
            }
            else
            {
                ViewData["FilmFk"] = new SelectList(aRWContext.Film.OrderBy(f => f.MovieTitle), "FilmPk", "MovieTitle");

                return View();
            }
        }
        public IActionResult Index()
        {
            return View();
        }
    }
}