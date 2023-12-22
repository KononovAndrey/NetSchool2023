using System.Net.Http.Json;
using NetSchool.Web.Pages.Books.Models;

namespace NetSchool.Web.Pages.Books.Services;

public class BookService(HttpClient httpClient) : IBookService
{
    public async Task<IEnumerable<BookModel>> GetBooks()
    {
        var response = await httpClient.GetAsync("v1/book");
        if (!response.IsSuccessStatusCode)
        {
            var content = await response.Content.ReadAsStringAsync();
            throw new Exception(content);
        }

        return await response.Content.ReadFromJsonAsync<IEnumerable<BookModel>>() ?? new List<BookModel>();
    }

    public async Task<BookModel> GetBook(Guid bookId)
    {
        var response = await httpClient.GetAsync($"v1/book/{bookId}");
        if (!response.IsSuccessStatusCode)
        {
            var content = await response.Content.ReadAsStringAsync();
            throw new Exception(content);
        }

        return await response.Content.ReadFromJsonAsync<BookModel>() ?? new();
    }

    public async Task AddBook(CreateModel model)
    {

        var requestContent = JsonContent.Create(model);
        var response = await httpClient.PostAsync("v1/book", requestContent);

        var content = await response.Content.ReadAsStringAsync();

        if (!response.IsSuccessStatusCode)
        {
            throw new Exception(content);
        }
    }

    public async Task EditBook(Guid bookId, UpdateModel model)
    {
        var requestContent = JsonContent.Create(model);
        var response = await httpClient.PutAsync($"v1/book/{bookId}", requestContent);

        var content = await response.Content.ReadAsStringAsync();

        if (!response.IsSuccessStatusCode)
        {
            throw new Exception(content);
        }
    }

    public async Task DeleteBook(Guid bookId)
    {
        var response = await httpClient.DeleteAsync($"v1/book/{bookId}");

        var content = await response.Content.ReadAsStringAsync();

        if (!response.IsSuccessStatusCode)
        {
            throw new Exception(content);
        }
    }
    
    public async Task<IEnumerable<AuthorModel>> GetAuthorList()
    {
        var response = await httpClient.GetAsync("/v1/author");
        var content = await response.Content.ReadAsStringAsync();

        if (!response.IsSuccessStatusCode)
        {
            throw new Exception(content);
        }

        return await response.Content.ReadFromJsonAsync<IEnumerable<AuthorModel>>() ?? new List<AuthorModel>();
    }
}