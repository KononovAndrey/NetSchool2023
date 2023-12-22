using NetSchool.Web.Pages.Books.Models;

namespace NetSchool.Web.Pages.Books.Services;

public interface IBookService
{
    Task<IEnumerable<BookModel>> GetBooks();
    Task<BookModel> GetBook(Guid bookId);
    Task AddBook(CreateModel model);
    Task EditBook(Guid bookId, UpdateModel model);
    Task DeleteBook(Guid bookId);
    Task<IEnumerable<AuthorModel>> GetAuthorList();
}