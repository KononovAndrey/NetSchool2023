using AutoMapper;
using Microsoft.EntityFrameworkCore;
using NetSchool.Context.Entities;
using NetSchool.Context;

namespace NetSchool.Services.Books;

public class BookModel
{
    public Guid Id { get; set; }

    public Guid AuthorId { get; set; }
    public string AuthorName { get; set; }

    public string Title { get; set; }
    public string Description { get; set; }

    public IEnumerable<string> Categories { get; set; }
}


public class BookModelProfile : Profile
{
    public BookModelProfile()
    {
        CreateMap<Book, BookModel>()
            .BeforeMap<BookModelActions>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.AuthorId, opt => opt.Ignore())
            .ForMember(dest => dest.AuthorName, opt => opt.Ignore())
            .ForMember(dest => dest.Categories, opt => opt.Ignore())
            ;
    }

    public class BookModelActions : IMappingAction<Book, BookModel>
    {
        private readonly IDbContextFactory<MainDbContext> contextFactory;

        public BookModelActions(IDbContextFactory<MainDbContext> contextFactory)
        {
            this.contextFactory = contextFactory;
        }

        public void Process(Book source, BookModel destination, ResolutionContext context)
        {
            using var db = contextFactory.CreateDbContext();

            var book = db.Books.Include(x => x.Author).ThenInclude(x => x.Detail).FirstOrDefault(x => x.Id == source.Id);

            destination.Id = book.Uid;
            destination.AuthorId = book.Author.Uid;
            destination.AuthorName = book.Author.Name + " " + book.Author.Detail.Family;
            destination.Categories = book.Categories?.Select(x => x.Title);
        }
    }
}