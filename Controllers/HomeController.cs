using Cinemanage.Data;
using Cinemanage.Enums;
using Cinemanage.Models;
using Cinemanage.Models.Database;
using Cinemanage.Models.TMDB;
using Cinemanage.Models.ViewModels;
using Cinemanage.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace Cinemanage.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _context;
        private readonly IRemoteMovieService _tmdbMovieService;
        private readonly UserManager<AppUser> _userManager;

        public HomeController(ILogger<HomeController> logger, ApplicationDbContext context, IRemoteMovieService tmdbMovieService, UserManager<AppUser> userManager)
        {
            _logger = logger;
            _context = context;
            _tmdbMovieService = tmdbMovieService;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            const int count = 15;
            string appUserId = _userManager.GetUserId(User);

            if (appUserId != null)
            {
                AppUser? appUser = await _context.Users.Include(u => u.Collections).FirstOrDefaultAsync(u => u.Id == appUserId);




                var data = new LandingPageVM()
                {

                    NowPlaying = await _tmdbMovieService.SearchMoviesAsync(MovieCategory.now_playing, count),
                    Popular = await _tmdbMovieService.SearchMoviesAsync(MovieCategory.popular, count),
                    TopRated = await _tmdbMovieService.SearchMoviesAsync(MovieCategory.top_rated, count),
                    Upcoming = await _tmdbMovieService.SearchMoviesAsync(MovieCategory.upcoming, count),
                    CustomCollections = new List<Collection>()
                };

                if (appUser != null)
                {
                    data.CustomCollections = appUser.Collections.ToList();

                }
                else
                {
                    data.CustomCollections = new List<Collection>(); // Initialize an empty list or handle it as per your requirements
                }



                ViewData["CustomCollections"] = new SelectList(_context.Collection.Where(c => c.Name != "All" && c.AppUserId == appUserId), "Id", "Name", data.CustomCollections);

                return View(data);
            }
            
            var data2 = new LandingPageVM()
            {

                NowPlaying = await _tmdbMovieService.SearchMoviesAsync(MovieCategory.now_playing, count),
                Popular = await _tmdbMovieService.SearchMoviesAsync(MovieCategory.popular, count),
                TopRated = await _tmdbMovieService.SearchMoviesAsync(MovieCategory.top_rated, count),
                Upcoming = await _tmdbMovieService.SearchMoviesAsync(MovieCategory.upcoming, count),
                CustomCollections = new List<Collection>()
            };


            return View(data2);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}