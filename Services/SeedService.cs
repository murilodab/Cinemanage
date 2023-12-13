using Cinemanage.Data;
using Cinemanage.Enums;
using Cinemanage.Models.Database;
using Cinemanage.Models.Settings;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace Cinemanage.Services
{
    public class SeedService
    {
        private readonly AppSettings _appSettings;
        private readonly ApplicationDbContext _dbContext;
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public SeedService(IOptions<AppSettings> appSettings, ApplicationDbContext dbContext, UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _appSettings = appSettings.Value;
            _dbContext = dbContext;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task ManageDataAsync(IServiceProvider svcProvider) //Wrapper
        {
            var dbContextSvc = svcProvider.GetRequiredService<ApplicationDbContext>();


            //Migration: This is the programmatic equivalent to Update-Database
            await dbContextSvc.Database.MigrateAsync();
            //Create the DB from the Migrations
            await SeedRolesAsync();
            await SeedUsersAsync();
            await SeedCollections();
        }

        private async Task SeedRolesAsync()
        {
            if (_dbContext.Roles.Any()) return;

            foreach (var role in Enum.GetNames(typeof(AppRoles)))
            {
                //Use the Role Manager to create roles
                await _roleManager.CreateAsync(new IdentityRole(role));

            }

            //var adminRole = _appSettings.CinemanageSettings.DefaultCredentials.Role;

            //await _roleManager.CreateAsync(new IdentityRole(adminRole));
        }

        private async Task SeedUsersAsync()
        {
            if (_userManager.Users.Any()) return;

            var credentials = _appSettings.CinemanageSettings.DefaultCredentials;

            //Admin
            //Step 1: Creates a new instance of BlogUser
            var adminUser = new AppUser()
            {
                Email = "mbtestcoder@gmail.com",
                UserName = "mbtestcoder@gmail.com",
                FirstName = "Murilo",
                LastName = "Barbosa",
                EmailConfirmed = true
            };

            //Step 2: Use the UserManager to create a new user that is defined by the adminUser
            await _userManager.CreateAsync(adminUser, "@Admin5023");

            //Step 3: Add this new user to the Administrator Role
            await _userManager.AddToRoleAsync(adminUser, AppRoles.Administrator.ToString());
        }

        private async Task SeedCollections()
        {
            if (_dbContext.Collection.Any()) return;

            _dbContext.Add(new Collection()
            {
                Name = _appSettings.CinemanageSettings.DefaultCollection.Name,
                Description = _appSettings.CinemanageSettings.DefaultCollection.Description

            });

            await _dbContext.SaveChangesAsync();

        }
    }

}
