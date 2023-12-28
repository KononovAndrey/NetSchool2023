namespace NetSchool.Test.Api;

using Microsoft.EntityFrameworkCore;
using NetSchool.Context;
using NetSchool.Context.Entities;
using System;
using System.Threading.Tasks;

public static class BookHelper
{
    public static async Task<Book> Create(IDbContextFactory<MainDbContext> contextFactory, Book book = null)
    {
        using var context = await contextFactory.CreateDbContextAsync();
        
        await context.Books.AddAsync(book);
        await context.SaveChangesAsync();
        
        return book;
    }

    public static async Task<Book> Get(IDbContextFactory<MainDbContext> contextFactory, Guid bookId)
    {
        using var context = await contextFactory.CreateDbContextAsync();

        return await context.Books.FirstOrDefaultAsync(x => x.Uid == bookId);
    }

    public static async Task Remove(IDbContextFactory<MainDbContext> contextFactory, Book book)
    {
        using var context = await contextFactory.CreateDbContextAsync();

        context.Books.Remove(book);
        await context.SaveChangesAsync();
    }

    public static async Task RemoveAll(IDbContextFactory<MainDbContext> contextFactory)
    {
        using var context = await contextFactory.CreateDbContextAsync();
        context.Books.RemoveRange(context.Books);
        await context.SaveChangesAsync();
    }
}