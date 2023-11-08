using Cinemanage.Data;
using Cinemanage.Models.Database;
using Cinemanage.Models.Settings;
using Cinemanage.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Microsoft.Extensions.Options;

namespace Cinemanage.Controllers
{
    public class MoviesController : Controller
    {
        private readonly AppSettings _appSettings;
        private readonly ApplicationDbContext _context;
        private readonly IImageService _imageService;
        private readonly IRemoteMovieService _tmdbMovieService;
        private readonly IDataMappingService _tmdbMappingService;



        public MoviesController(IOptions<AppSettings> appSettings, ApplicationDbContext context, IImageService imageService, IRemoteMovieService tmdbMovieService, IDataMappingService tmdbMappingService)
        {
            _appSettings = appSettings.Value;
            _context = context;
            _imageService = imageService;
            _tmdbMovieService = tmdbMovieService;
            _tmdbMappingService = tmdbMappingService;
        }

        public async Task<IActionResult> Import()
        {
            var movies = await _context.Movie.ToListAsync();
            return View(movies);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Import(int id)
        {
            //If we already have this movie, warn the user instead of importing it again
            if (_context.Movie.Any(m => m.MovieId == id))
            {
                var localMovie = await _tmdbMovieService.MovieDetailAsync(id);
                return RedirectToAction("Details", "Movies", new { id = localMovie.Id, local = true });
            }

            //Get the raw data from the API
            var movieDetail = await _tmdbMovieService.MovieDetailAsync(id);

            //Run the data through a mapping procedure
            var movie = await _tmdbMappingService.MapMovieDetailAsync(movieDetail);

            //Add the new Movie
            _context.Add(movie);
            await _context.SaveChangesAsync();

            //Assign it to the default All Collection
            await AddToMovieCollection(movie.Id, _appSettings.CinemanageSettings.DefaultCollection.Name);

            return RedirectToAction("Import");
        }

        public async Task<IActionResult> Library()
        {
            var movies = await _context.Movie.ToListAsync();
            return View(movies);
        }


        private async Task AddToMovieCollection(int movieId, string collectionName)
        {
            var collection = await _context.Collection.FirstOrDefaultAsync(c => c.Name == collectionName);
            _context.Add(
                new MovieCollection()
                {
                    CollectionId = collection.Id,
                    MovieId = movieId
                }

                );
            await _context.SaveChangesAsync();
        }

        private async Task AddToMovieColleciton(int movieId, int collectionId)
        {
            _context.Add(
                new MovieCollection()
                {
                    CollectionId = collectionId,
                    MovieId = movieId
                });

            await _context.SaveChangesAsync();
        }


    }

}
