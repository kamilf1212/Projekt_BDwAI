using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Projekt_BDwAI.Areas.Identity.Data;
using Projekt_BDwAI.Models;

namespace Projekt_BDwAI.Data;

public class Project_context : IdentityDbContext<User>
{
    public Project_context(DbContextOptions<Project_context> options)
        : base(options)
    {
    }

    // Add DbSet properties for domain models
    public DbSet<Author> Authors { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<Book> Books { get; set; }
    public DbSet<Loan> Loans { get; set; }

    private class UserEntityConfiguration :
    IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.Property(x => x.FirstName).HasMaxLength(255);
            builder.Property(x => x.LastName).HasMaxLength(255);
        }
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        // Customize the ASP.NET Identity model and override the defaults if needed.
        // For example, you can rename the ASP.NET Identity table names and more.
        // Add your customizations after calling base.OnModelCreating(builder);
    }
}
