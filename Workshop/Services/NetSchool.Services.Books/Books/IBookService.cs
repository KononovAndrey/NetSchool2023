namespace NetSchool.Services.Books;

public interface IBookService
{
    Task<IEnumerable<BookModel>> GetAll();
    Task<BookModel> GetById(Guid id);
    Task<BookModel> Create(CreateModel model);
    Task Update(Guid id, UpdateModel model);
    Task Delete(Guid id);
}
