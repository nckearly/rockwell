using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

//for authentication
using Microsoft.EntityFrameworkCore;
using Rockwell.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Security.Claims;


//for authorization
using Microsoft.AspNetCore.Authorization;

namespace Rockwell.Controllers
{
    public class AccountController : Controller
    {
        private readonly RWStudiosContext _context;

        public AccountController(RWStudiosContext context)
        {
            _context = context;
        }

        public IActionResult Login(string returnURL)
        {
            returnURL = string.IsNullOrEmpty(returnURL) ? "~/Account" : returnURL;

            return View(new LoginInput { ReturnURL = returnURL });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login([Bind("Username, UserPassword, ReturnURL")] LoginInput loginInput)
        {
            if (ModelState.IsValid)
            {
                var aUser = await _context.Contact.Include(u => u.UserRoleFkNavigation).FirstOrDefaultAsync(u => u.UserLogin == loginInput.Username && u.UserPassword == loginInput.UserPassword);

                if(aUser != null)
                {
                    var claims = new List<Claim>();
                    claims.Add(new Claim(ClaimTypes.Name, aUser.FirstName));
                    claims.Add(new Claim(ClaimTypes.Sid, aUser.ContactPk.ToString()));
                    claims.Add(new Claim(ClaimTypes.Role, aUser.UserRoleFkNavigation.UserRoleName));

                    var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

                    var principal = new ClaimsPrincipal(identity);

                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

                    return Redirect(loginInput.ReturnURL);


                }
                else
                {
                    ViewData["message"] = "Invalid credentials.";
                }
            }
            return View(loginInput);
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Home");
        }

        [Authorize]
        public async Task<IActionResult> Index()
        {
            int userPK = Int32.Parse(HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Sid).Value);
            var myReviews = _context.FilmReview.Include(r => r.FilmFkNavigation).Where(r=>r.ContactFk == userPK).OrderBy(r=>r.FilmFkNavigation.MovieTitle);

            var name = HttpContext.User.Claims.FirstOrDefault(u => u.Type == ClaimTypes.Name).Value;


            ViewData["namefirst"] = name;
            return View(await myReviews.ToListAsync());
        }

        public IActionResult SignUp()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SignUp([Bind("UserLogin, UserPassword, FirstName, LastName")] Contact aContact)
        {
            if (ModelState.IsValid)
            {
                var aUser = await _context.Contact.FirstOrDefaultAsync(u => u.UserLogin == aContact.UserLogin);

                if(aUser is null)
                {
                    _context.Add(aContact);
                    await _context.SaveChangesAsync();

                    TempData["message"] = "Thanks for registering, please log in";
                    return RedirectToAction("Login");
                }
                else
                {
                    ViewData["duplicatemessage"] = "Please choose a different username";
                }
            }

            return View(aContact);
        }

        [Authorize]
        public async Task<IActionResult>CreateReview(int filmpk)
        {
            var oneFilm = await _context.Film.FirstOrDefaultAsync(f => f.FilmPk == filmpk);

            if( oneFilm == null)
            {
                return RedirectToAction("IndexPublic", "Films");
            }

            ViewData["FilmFk"] = filmpk;
            ViewData["FilmTitle"] = oneFilm.MovieTitle;
            return View();
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateReview([Bind("ReviewSummary, ReviewRating, FilmFk")] FilmReview aReview)
        {
            if (ModelState.IsValid)
            {
                int userPK = Int32.Parse(HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Sid).Value);
                aReview.ContactFk = userPK;
                _context.Add(aReview);
                await _context.SaveChangesAsync();

                var reviewedFilm = await _context.Film.FirstOrDefaultAsync(f => f.FilmPk == aReview.FilmFk);
                TempData["message"] = $"Review of {reviewedFilm.MovieTitle} added successfully";
                return RedirectToAction("Index", "Account");
            }
            else
            {
                return View();
            }
        }

        [Authorize]
        public async Task<IActionResult> DeleteReview (int reviewpk)
        {
            var review = await _context.FilmReview.Include(r => r.FilmFkNavigation).FirstOrDefaultAsync(r => r.ReviewPk == reviewpk);

            if(review == null)
            {
                return RedirectToAction("Index", "Account");
            }

            int userPK = Int32.Parse(HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Sid).Value);
            if(review.ContactFk != userPK)
            {
                return RedirectToAction("Index", "Account");
            }

            _context.Remove(review);
            await _context.SaveChangesAsync();

            TempData["message"] = $"Review of {review.FilmFkNavigation.MovieTitle} delete.";
            return RedirectToAction("Index", "Account");
        }
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> ReviewsAdmin()
        {
            var reviews = _context.FilmReview
                .Include(r => r.ContactFkNavigation)
                .Include(r => r.FilmFkNavigation)
                .OrderByDescending(r => r.ReviewDate);

            return View(await reviews.ToListAsync());
        }
    }
}