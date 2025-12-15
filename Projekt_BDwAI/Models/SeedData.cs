using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Projekt_BDwAI.Areas.Identity.Data;

namespace Projekt_BDwAI.Models
{
    public class SeedData
    {
        public static async void Initialize(IServiceScope scope)
        {
            var services = scope.ServiceProvider;

            var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var roles = new[] { "Admin", "User" };
            foreach (var role in roles)
            {
                if (!await roleManager.RoleExistsAsync(role))
                {
                    await roleManager.CreateAsync(new IdentityRole(role));
                }
            }

            var userManager = scope.ServiceProvider.GetRequiredService<UserManager<User>>();
            string email = "admin@admin.com";
            string password = "Admin123!";
            if (await userManager.FindByEmailAsync(email) == null)
            {
                var adminUser = new User
                {
                    FirstName = "Jan",
                    LastName = "Kowalski",
                    Email = email,
                    UserName = email
                };
                await userManager.CreateAsync(adminUser, password);
                await userManager.AddToRoleAsync(adminUser, "Admin");
            }
        }
    }
}
