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

        public async Task ManageDataAsync()
        {
            await UpdateDatabaseAsync();
            await SeedRolesAsync();
            await SeedUsersAsync();
            await SeedCollections();
        }


        private async Task UpdateDatabaseAsync()
        {
            await _dbContext.Database.MigrateAsync();
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

            var newUser = new AppUser()
            {
                FirstName = credentials.FirstName,
                LastName = credentials.LastName,
                Email = credentials.Email,
                UserName = credentials.Email,
                EmailConfirmed = true
            };

            await _userManager.CreateAsync(newUser, credentials.Password);
            await _userManager.AddToRoleAsync(newUser, credentials.Role);
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
