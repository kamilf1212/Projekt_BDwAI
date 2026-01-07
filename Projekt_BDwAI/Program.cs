using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Projekt_BDwAI.Areas.Identity.Data;
using Projekt_BDwAI.Data;
using Projekt_BDwAI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Authorization;


namespace Projekt_BDwAI
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            //var connectionString = builder.Configuration.GetConnectionString("Project_contextConnection") ?? throw new InvalidOperationException("Connection string 'Project_contextConnection' not found."); ;


            builder.Services.AddDbContext<Project_context>(options => options.UseSqlite(builder.Configuration.GetConnectionString("Project_contextConnection")));

            //builder.Services.AddDbContext<Project_context>(options => options.UseSqlite(connectionString));

            // required confirmed account false to login without email confirmation
            builder.Services
                .AddDefaultIdentity<User>(options => options.SignIn.RequireConfirmedAccount = false)
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<Project_context>();

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            // Authentication must come before Authorization
            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}")
                .WithStaticAssets();

            app.MapRazorPages();
            app.MapStaticAssets();

            // Ensure DB schema is up-to-date and run seeding (awaited)
            using (var scope = app.Services.CreateScope())
            {
                var db = scope.ServiceProvider.GetRequiredService<Project_context>();
                await db.Database.MigrateAsync(); // apply migrations

                DbInitializer.Seed(db);

                await SeedData.InitializeAsync(scope);
                //await Task.Run(() => SeedData.Initialize(scope));
            }

            app.Run();
        }
    }
}