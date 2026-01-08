using Microsoft.AspNetCore.Identity;
using Projekt_BDwAI.Areas.Identity.Data;
using Projekt_BDwAI.Data;
using Projekt_BDwAI.Models;

public static class DbInitializer
{
    public static void Seed(Project_context context)
    {
        // AUTORZY
        if (!context.Authors.Any())
        {
            var authors = new List<Author>
            {
                new Author { Name = "Adam Mickiewicz", Biography = "Polski poeta romantyczny" },
                new Author { Name = "Henryk Sienkiewicz", Biography = "Laureat Nagrody Nobla" }
            };
            context.Authors.AddRange(authors);
            context.SaveChanges();
        }

        // KATEGORIE
        if (!context.Categories.Any())
        {
            var categories = new List<Category>
            {
                new Category { Name = "Powieść" },
                new Category { Name = "Poezja" }
            };
            context.Categories.AddRange(categories);
            context.SaveChanges();
        }

        // KSIĄŻKI
        if (!context.Books.Any())
        {
            var mickiewicz = context.Authors.First(a => a.Name == "Adam Mickiewicz");
            var sienkiewicz = context.Authors.First(a => a.Name == "Henryk Sienkiewicz");

            var powiesc = context.Categories.First(c => c.Name == "Powieść");
            var poezja = context.Categories.First(c => c.Name == "Poezja");

            var books = new List<Book>
            {
                new Book
                {
                    Title = "Pan Tadeusz",
                    ISBN = "1234567890123",
                    AuthorId = mickiewicz.Id,
                    CategoryId = poezja.Id
                },
                new Book
                {
                    Title = "Quo Vadis",
                    ISBN = "1234567890125",
                    AuthorId = sienkiewicz.Id,
                    CategoryId = powiesc.Id
                },
                new Book
                {
                    Title = "Potop",
                    ISBN = "1234567890126",
                    AuthorId = sienkiewicz.Id,
                    CategoryId = powiesc.Id
                }
            };

            context.Books.AddRange(books);
            context.SaveChanges();
        }
    }

    public static async Task SeedUsers(IServiceScope scope)
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

        // ADMINISTRATOR
        string adminEmail = "admin@admin.com";
        string adminPassword = "Admin123!";
        if (await userManager.FindByEmailAsync(adminEmail) == null)
        {
            var adminUser = new User
            {
                FirstName = "Jan",
                LastName = "Kowalski",
                Email = adminEmail,
                UserName = adminEmail
            };
            await userManager.CreateAsync(adminUser, adminPassword);
            await userManager.AddToRoleAsync(adminUser, "Admin");
        }

        // STANDARDOWY UŻYTKOWNIK
        string userEmail = "user@user.com";
        string userPassword = "User123!";
        if (await userManager.FindByEmailAsync(userEmail) == null)
        {
            var standardUser = new User
            {
                FirstName = "Michał",
                LastName = "Kowalski",
                Email = userEmail,
                UserName = userEmail
            };
            await userManager.CreateAsync(standardUser, userPassword);
            await userManager.AddToRoleAsync(standardUser, "User");
        }
    }
}
