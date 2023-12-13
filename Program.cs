using Cinemanage.Data;
using Cinemanage.Models.Settings;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Cinemanage.Services;
using Cinemanage.Services.Interfaces;
using Cinemanage.Models.Database;
using Cinemanage.Helpers;

var builder = WebApplication.CreateBuilder(args);
var connectionString = ConnectionHelper.GetConnectionString(builder.Configuration) ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

// Add services to the container.

builder.Services.AddRazorPages()
    .AddRazorRuntimeCompilation();

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(connectionString));

AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddDefaultIdentity<AppUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddRoles<IdentityRole>()
    .AddDefaultUI()
    .AddDefaultTokenProviders()
    .AddEntityFrameworkStores<ApplicationDbContext>();

builder.Services.AddControllersWithViews();

var appSettings = builder.Configuration.GetSection("AppSettings");

builder.Services.Configure<AppSettings>(appSettings);

builder.Services.AddTransient<SeedService>();

builder.Services.AddHttpClient();
builder.Services.AddScoped<IRemoteMovieService, TMDBMovieService>();

builder.Services.AddScoped<IDataMappingService, TMDBMappingService>();

builder.Services.AddSingleton<IImageService, BasicImageService>();


var app = builder.Build();

//Using the Custom DataService
using (var serviceScope = app.Services.CreateScope())
{
    var services = serviceScope.ServiceProvider;
    var dataService = services.GetRequiredService<SeedService>();
    await dataService.ManageDataAsync(serviceScope.ServiceProvider);
}




// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();

app.Run();
