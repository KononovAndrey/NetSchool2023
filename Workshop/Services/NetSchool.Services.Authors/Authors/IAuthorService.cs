namespace NetSchool.Services.Authors;

public interface IAuthorService
{
    Task<IEnumerable<AuthorModel>> GetAll();
}
