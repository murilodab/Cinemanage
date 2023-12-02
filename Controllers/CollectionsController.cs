using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Cinemanage.Data;
using Cinemanage.Models.Database;
using Cinemanage.Models.Settings;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

namespace Cinemanage.Controllers
{
    public class CollectionsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly AppSettings _appSettings;
        private readonly UserManager<AppUser> _userManager;

        public CollectionsController(ApplicationDbContext context, IOptions<AppSettings> appSettings, UserManager<AppUser> userManager)
        {
            _context = context;
            _appSettings = appSettings.Value;
            _userManager = userManager;
        }



        // GET: Collections
        [Authorize]
        public async Task<IActionResult> Index()
        {

            string appUserId = _userManager.GetUserId(User);

            AppUser? appUser = _context.Users
                                       .Include(c => c.Collections)
                                       .ThenInclude(c => c.MovieCollections)
                                       .FirstOrDefault(u => u.Id == appUserId);

            var defaultCollectionName = _appSettings.CinemanageSettings.DefaultCollection.Name;

            var collections = await _context.Collection.Where(c => c.AppUserId == appUserId)
                                                       .Include(c => c.Name != defaultCollectionName)
                                                       .ToListAsync();
            
            
            return View(collections);
        }

       

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Collection == null)
            {
                return NotFound();
            }

            var collection = await _context.Collection
                .FirstOrDefaultAsync(m => m.Id == id);
            if (collection == null)
            {
                return NotFound();
            }

            if (collection.Name == _appSettings.CinemanageSettings.DefaultCollection.Name)
            {
                return RedirectToAction("Index", "Collections");
            }

            return View(collection);
        }

        // GET: Blogs/Create
        [Authorize]
        public IActionResult Create()
        {

            return View();
        }

        // POST: Collections/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Description,AppUserId")] Collection collection)
        {
            ModelState.Remove("AppUserId");

            if (ModelState.IsValid)
            {
                string appUserId = _userManager.GetUserId(User);
                collection.AppUserId = appUserId;


                _context.Add(collection);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index", "Collections", new { id = collection.Id });
            }

            return View(collection);
        }

        // GET: Collections/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Collection == null)
            {
                return NotFound();
            }

            string appUserId = _userManager.GetUserId(User);

            var collection = await _context.Collection.Where(c => c.Id == id && c.AppUserId == appUserId)
                                            .FirstOrDefaultAsync();
            
            if (collection == null)
            {
                return NotFound();
            }
            return View(collection);
        }

        // POST: Collections/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Description, AppUserId")] Collection collection)
        {
            if (id != collection.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    if(collection.Name == _appSettings.CinemanageSettings.DefaultCollection.Name)
                    {
                        return RedirectToAction("Index", "Collections");
                    }

                    string appUserId = _userManager.GetUserId(User);
                    collection.AppUserId = appUserId;

                    _context.Update(collection);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CollectionExists(collection.Id))
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
            return View(collection);
        }

        // GET: Collections/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Collection == null)
            {
                return NotFound();
            }


            string appUserId = _userManager.GetUserId(User);

            var collection = await _context.Collection
                .FirstOrDefaultAsync(m => m.Id == id & m.AppUserId == appUserId);

            if (collection == null)
            {
                return NotFound();
            }

            if (collection.Name == _appSettings.CinemanageSettings.DefaultCollection.Name)
            {
                return RedirectToAction("Index", "Collections");
            }

            return View(collection);
        }

        // POST: Collections/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {


            if (_context.Collection == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Collection'  is null.");
            }

            string appUserId = _userManager.GetUserId(User);

            var collection = await _context.Collection.FirstOrDefaultAsync(c => c.Id == id && c.AppUserId == appUserId);
            
            if (collection != null)
            {
                _context.Collection.Remove(collection);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction("Index", "Collections");
        }

        private bool CollectionExists(int id)
        {
          return (_context.Collection?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
