using AutoMapper;
using Microsoft.EntityFrameworkCore;
using NetSchool.Common.Exceptions;
using NetSchool.Common.Validator;
using NetSchool.Context;
using NetSchool.Context.Entities;
using NetSchool.Services.Actions;

namespace NetSchool.Services.Books;

public class BookService : IBookService
{
    private readonly IDbContextFactory<MainDbContext> dbContextFactory;
    private readonly IMapper mapper;
    private readonly IAction action;
    private readonly IModelValidator<CreateModel> createModelValidator;
    private readonly IModelValidator<UpdateModel> updateModelValidator;

    public BookService(
        IDbContextFactory<MainDbContext> dbContextFactory, 
        IMapper mapper,
        IAction action,
        IModelValidator<CreateModel> createModelValidator,
        IModelValidator<UpdateModel> updateModelValidator
        )
    {
        this.dbContextFactory = dbContextFactory;
        this.mapper = mapper;
        this.action = action;
        this.createModelValidator = createModelValidator;
        this.updateModelValidator = updateModelValidator;
    }

    public async Task<IEnumerable<BookModel>> GetAll()
    {
        using var context = await dbContextFactory.CreateDbContextAsync();

        var books = await context.Books
            .Include(x => x.Author).ThenInclude(x => x.Detail)
            .Include(x => x.Categories)
            .ToListAsync();

        var result = mapper.Map<IEnumerable<BookModel>>(books);

        return result;
    }

    public async Task<BookModel> GetById(Guid id)
    {
        using var context = await dbContextFactory.CreateDbContextAsync();

        var book = await context.Books
            .Include(x => x.Author).ThenInclude(x => x.Detail)
            .Include(x => x.Categories)
            .FirstOrDefaultAsync(x => x.Uid == id);

        var result = mapper.Map<BookModel>(book);

        return result;
    }

    public async Task<BookModel> Create(CreateModel model)
    {
        await createModelValidator.CheckAsync(model);

        using var context = await dbContextFactory.CreateDbContextAsync();

        var book = mapper.Map<Book>(model);

        await context.Books.AddAsync(book);

        await context.SaveChangesAsync();

        await action.PublicateBook(new PublicateBookModel()
        {
            Id = book.Id,
            Title = book.Title,
            Description = book.Description
        });

        return mapper.Map<BookModel>(book);
    }

    public async Task Update(Guid id, UpdateModel model)
    {
        await updateModelValidator.CheckAsync(model);

        using var context = await dbContextFactory.CreateDbContextAsync();

        var book = await context.Books.Where(x => x.Uid == id).FirstOrDefaultAsync();

        book = mapper.Map(model, book);

        context.Books.Update(book);

        await context.SaveChangesAsync();
    }

    public async Task Delete(Guid id)
    {
        using var context = await dbContextFactory.CreateDbContextAsync();

        var book = await context.Books.Where(x => x.Uid == id).FirstOrDefaultAsync();

        if (book == null)
            throw new ProcessException($"Book (ID = {id}) not found.");

        context.Books.Remove(book);

        await context.SaveChangesAsync();
    }
}
