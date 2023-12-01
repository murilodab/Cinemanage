using Cinemanage.Data;
using Cinemanage.Models.Database;
using Cinemanage.Models.Settings;
using Cinemanage.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using X.PagedList;

namespace Cinemanage.Controllers
{
    public class MovieCollectionsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public MovieCollectionsController(ApplicationDbContext context)
        {
            _context = context;
        }

        //public async Task<IActionResult> Index(int? id)
        //{
        //    id ??= (await _context.Collection.FirstOrDefaultAsync(c => c.Name.ToUpper() == "ALL")).Id;

        //    ViewData["CollectionId"] = new SelectList(_context.Collection, "Id", "Name", id);

        //    var allmovieIds = await _context.Movie.Select(m => m.Id).ToListAsync();

        //    var movieIdsInCollection = await _context.MovieCollection
        //                                              .Where(m => m.CollectionId == id)
        //                                              .OrderBy(m => m.Order)
        //                                              .Select(m => m.MovieId)
        //                                              .ToListAsync();

        //    var movieIdsNotInCollection = allmovieIds.Except(movieIdsInCollection);

        //    var moviesInCollection = new List<Movie>();
        //    movieIdsInCollection.ForEach(movieId => moviesInCollection.Add(_context.Movie.Find(movieId)));

        //    ViewData["IdsInCollection"] = new MultiSelectList(moviesInCollection, "Id", "Title");

        //    var moviesNotInCollection = await _context.Movie.AsNoTracking().Where(m => movieIdsNotInCollection.Contains(m.Id)).ToListAsync();
        //    ViewData["IdsNotInCollection"] = new MultiSelectList(moviesNotInCollection, "Id", "Title");

        //    return View();
        //}

        public async Task<IActionResult> Index(int? page, int? id)
        {

            var collectionExists = _context.MovieCollection.Any(c => c.CollectionId == id);

            if (!collectionExists)
            {
                return NotFound(); // or handle accordingly
            }

            var pageNumber = page ?? 1;
            var pageSize = 5;

            var movies = await _context.MovieCollection
                                .Include(c => c.Collection)
                                .Include(mc => mc.Movie)
                                .Where(c => c.CollectionId == id)
                                .OrderBy(m => m.Movie.Title) // or any other ordering you need
                                .ToPagedListAsync(pageNumber, pageSize);

            if(movies.Any())
{
                var movieTitles = movies.Select(m => m.Movie.Title).ToList();

            }
            else
            {
                return NotFound();
            }
            
            ViewData["Title"] = movies.Select(c => c.Collection.Name).FirstOrDefault().ToString();

            return View(movies);
        }

        // POST: Collections/Delete/5
        
        public async Task<IActionResult> DeleteConfirmed(int id, int collectionId)
        {
            if (_context.Collection == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Collection'  is null.");
            }

            var movieCollection = _context.MovieCollection.Where(m => m.CollectionId == collectionId);

            var movie = movieCollection.Select(m => m.Movie).Where(m => m.MovieId == id).FirstOrDefault();

 
            if (movie != null)
            {
                _context.Movie.Remove(movie);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction("Index", new {id = collectionId});
        }

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Index(int id, List<int> idsInCollection)
        //{
        //    var oldRecords = _context.MovieCollection.Where(c => c.CollectionId == id);
        //    _context.MovieCollection.RemoveRange(oldRecords);
        //    await _context.SaveChangesAsync();

        //    if(idsInCollection != null)
        //    {
        //        int index = 1;
        //        idsInCollection.ForEach(movieId =>
        //        {
        //            _context.Add(new MovieCollection()
        //            {
        //                CollectionId = id,
        //                MovieId = movieId,
        //                Order = index++
        //            });
        //        });

        //        await _context.SaveChangesAsync();
        //    }

        //    return RedirectToAction(nameof(Index), new { id });
        //}


    }
}
