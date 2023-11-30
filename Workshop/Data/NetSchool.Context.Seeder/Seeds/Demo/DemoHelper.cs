namespace NetSchool.Context.Seeder;

using NetSchool.Context.Entities;

public class DemoHelper
{
    public IEnumerable<Book> GetBooks = new List<Book>
    {
        new Book()
        {
            Uid = Guid.NewGuid(),
            Title = "Harry Potter and the Philosopher's Stone",
            Author = new Author()
            {
                Uid = Guid.NewGuid(),
                Name = "Joanne",  
                Detail = new AuthorDetail()
                {
                    Country = "England",
                    Family = "Rowling",
                }
            },
            Categories = new List<Category>()
            {
                new Category()
                {
                    Title = "Child books",
                },
                new Category()
                {
                    Title = "Fantasy",
                }
            }
        },
    };
}