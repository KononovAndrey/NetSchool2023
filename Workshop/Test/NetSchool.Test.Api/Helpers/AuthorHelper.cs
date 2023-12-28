namespace NetSchool.Test.Api;

using Microsoft.EntityFrameworkCore;
using NetSchool.Context;
using NetSchool.Context.Entities;
using System;
using System.Threading.Tasks;

public static class AuthorHelper
{
    public static async Task<Author> Create(IDbContextFactory<MainDbContext> contextFactory, Author author = null)
    {
        using var context = await contextFactory.CreateDbContextAsync();
        
        await context.Authors.AddAsync(author);
        await context.SaveChangesAsync();
        
        return author;
    }

    public static async Task<Author> Get(IDbContextFactory<MainDbContext> contextFactory, Guid authorId)
    {
        using var context = await contextFactory.CreateDbContextAsync();

        return await context.Authors.FirstOrDefaultAsync(x => x.Uid == authorId);
    }

    public static async Task Remove(IDbContextFactory<MainDbContext> contextFactory, Author author)
    {
        using var context = await contextFactory.CreateDbContextAsync();

        context.AuthorDetails.RemoveRange(author.Detail);
        context.Authors.Remove(author);
        await context.SaveChangesAsync();
    }

    public static async Task RemoveAll(IDbContextFactory<MainDbContext> contextFactory)
    {
        using var context = await contextFactory.CreateDbContextAsync();
        context.AuthorDetails.RemoveRange(context.AuthorDetails);
        context.Authors.RemoveRange(context.Authors);
        await context.SaveChangesAsync();
    }
}