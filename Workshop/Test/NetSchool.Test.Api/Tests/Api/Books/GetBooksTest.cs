namespace NetSchool.Test.Api;

using FluentAssertions;
using NUnit.Framework;
using Refit;
using System.Net;
using System.Threading.Tasks;

[TestFixture]
public class GetBooksTest : ComponentTest
{
    [Test]
    public async Task GetAllBooks_WhenCalledUnautorized_ReturnsNotAutorized()
    {
        using var api = await CreateApiClient();

        var exception = Assert.CatchAsync<ApiException>(async () => await api.V1_Book_GetAll());

        exception.Should().NotBeNull();
        exception.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
    }

    [Test]
    public async Task GetAllBooks_WhenCalledAutorized_ReturnsData()
    {
        using var api = await CreateApiClientAndSignInAsAdmin();

        var author = await AuthorHelper.Create(_contextFactory, new Context.Entities.Author()
        {
            Id = 1,
            Uid = Guid.NewGuid(),
            Name = "Test",
            Detail = new Context.Entities.AuthorDetail()
            {
                Id = 1,
                Family = "Test"
            }
        });

        var book = await BookHelper.Create(_contextFactory, new Context.Entities.Book()
        {
            Id = 1,
            Uid = Guid.NewGuid(),
            Title = "Test",
            Description = "Test",
            AuthorId = author.Id,
        });

        var data = await api.V1_Book_GetAll();

        Assert.That(data, Is.Not.Null);
        Assert.That(data.Count(), Is.EqualTo(1));
    }

    [Test]
    public async Task GetAllBooks_WhenCalledAutorized_WithNoRights_ReturnsForbidden()
    {
        using var api = await CreateApiClientAndSignInAsGuest();

        var exception = Assert.CatchAsync<ApiException>(async () => await api.V1_Book_GetAll());

        exception.Should().NotBeNull();
        exception.StatusCode.Should().Be(HttpStatusCode.Forbidden);
    }

    [Test]
    public async Task GetBook_ValidRequest_InvalidId_NotFound()
    {
        using var api = await CreateApiClientAndSignInAsAdmin();

        var exception = Assert.CatchAsync<ApiException>(async () => await api.V1_Book_Get(Guid.NewGuid()));

        exception.Should().NotBeNull();
        exception.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }

    [Test]
    public async Task GetBook_ValidRequest_ValidId_Book()
    {
        using var api = await CreateApiClientAndSignInAsAdmin();

        var author = await AuthorHelper.Create(_contextFactory, new Context.Entities.Author()
        {
            Id = 1,
            Uid = Guid.NewGuid(),
            Name = "Test",       
            Detail = new Context.Entities.AuthorDetail()
            {
                Id = 1,
                Family = "Test"
            }
        });

        var book = await BookHelper.Create(_contextFactory, new Context.Entities.Book()
        {
            Id = 1,
            Uid = Guid.NewGuid(),
            Title = "Test",
            Description = "Test",
            AuthorId = author.Id,
        });

        var data = await api.V1_Book_Get(book.Uid);

        Assert.That(data, Is.Not.Null);
        Assert.That(data.Id, Is.EqualTo(book.Uid));
    }
}