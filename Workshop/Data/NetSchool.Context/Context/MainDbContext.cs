namespace NetSchool.Context;

using NetSchool.Context.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

public class MainDbContext : IdentityDbContext<User, IdentityRole<Guid>, Guid>
{
    public DbSet<Author> Authors { get; set; }
    public DbSet<AuthorDetail> AuthorDetails { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<Book> Books { get; set; }

    public MainDbContext(DbContextOptions<MainDbContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ConfigureAuthors();
        modelBuilder.ConfigureAuthorDetails();
        modelBuilder.ConfigureBooks();
        modelBuilder.ConfigureCategories();
        modelBuilder.ConfigureUsers();
    }
}
