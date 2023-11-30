namespace NetSchool.Context;

using Microsoft.EntityFrameworkCore;
using NetSchool.Context.Entities;

public static class AuthorsContextConfiguration
{
    public static void ConfigureAuthors(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Author>().ToTable("authors");
        modelBuilder.Entity<Author>().Property(x => x.Name).IsRequired();
        modelBuilder.Entity<Author>().Property(x => x.Name).HasMaxLength(50);
        modelBuilder.Entity<Author>().HasIndex(x => x.Name).IsUnique();
    }
}