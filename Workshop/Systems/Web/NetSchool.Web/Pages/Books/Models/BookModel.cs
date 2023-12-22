namespace NetSchool.Web.Pages.Books.Models;

public class BookModel
{
    public Guid Id { get; set; }

    public Guid AuthorId { get; set; }
    public string AuthorName { get; set; }

    public string Title { get; set; }
    public string Description { get; set; }

    public IEnumerable<string> Categories { get; set; }
}