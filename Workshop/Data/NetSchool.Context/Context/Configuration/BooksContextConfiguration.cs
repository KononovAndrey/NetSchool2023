namespace NetSchool.Context;

using Microsoft.EntityFrameworkCore;
using NetSchool.Context.Entities;

public static class BooksContextConfiguration
{
    public static void ConfigureBooks(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Book>().ToTable("books");
        modelBuilder.Entity<Book>().Property(x => x.Title).IsRequired();
        modelBuilder.Entity<Book>().Property(x => x.Title).HasMaxLength(250);
        modelBuilder.Entity<Book>().HasOne(x => x.Author).WithMany(x => x.Books).HasForeignKey(x => x.AuthorId).OnDelete(DeleteBehavior.Restrict);
    }
}