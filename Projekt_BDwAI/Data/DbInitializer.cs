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
            var author1 = context.Authors.First();
            var category1 = context.Categories.First();

            var books = new List<Book>
            {
                new Book
                {
                    Title = "Pan Tadeusz",
                    ISBN = 1234567890,
                    AuthorId = author1.Id,
                    CategoryId = category1.Id
                }
            };

            context.Books.AddRange(books);
            context.SaveChanges();
        }
    }
}
