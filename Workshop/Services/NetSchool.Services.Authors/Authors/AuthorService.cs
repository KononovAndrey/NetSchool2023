namespace NetSchool.Services.Authors;

using AutoMapper;
using Microsoft.EntityFrameworkCore;
using NetSchool.Context;

public class AuthorService : IAuthorService
{
    private readonly IDbContextFactory<MainDbContext> dbContextFactory;
    private readonly IMapper mapper;

    public AuthorService(IDbContextFactory<MainDbContext> dbContextFactory, 
        IMapper mapper
        )
    {
        this.dbContextFactory = dbContextFactory;
        this.mapper = mapper;
    }

    public async Task<IEnumerable<AuthorModel>> GetAll()
    {
        using var context = await dbContextFactory.CreateDbContextAsync();

        var author = await context.Authors
            .Include(x => x.Detail)
            .ToListAsync();

        var result = mapper.Map<IEnumerable<AuthorModel>>(author);

        return result;
    }
}
