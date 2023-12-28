namespace NetSchool.Test.Api;

using FluentAssertions;
using NetSchool.Common;
using NetSchool.Common.Extensions;
using NetSchool.Common.Responses;
using NUnit.Framework;
using Refit;
using System.Net;
using System.Threading.Tasks;

[TestFixture]
public class CreateBooksTest : ComponentTest
{
    [Test]
    public async Task AddBook_WhenCalledUnautorized_ReturnsNotAutorized()
    {
        using var api = await CreateApiClient();

        var exception = Assert.CatchAsync<ApiException>(async () => await api.V1_Book_Create(new Services.Books.CreateModel()));

        exception.Should().NotBeNull();
        exception.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
    }

    [Test]
    public async Task AddBook_WhenCalledAutorized_WithNoRights_ReturnsForbidden()
    {
        using var api = await CreateApiClientAndSignInAsUser();

        var exception = Assert.CatchAsync<ApiException>(async () => await api.V1_Book_Create(new Services.Books.CreateModel()));

        exception.Should().NotBeNull();
        exception.StatusCode.Should().Be(HttpStatusCode.Forbidden);
    }

    [Test]
    [TestCaseSource(typeof(BooksTestData), nameof(BooksTestData.ValidBookNames))]
    public async Task AddBook_ValidRequest_ValidData_Added(string name)
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

        var model = new Services.Books.CreateModel()
        {
            AuthorId = author.Uid,
            Title = name,
        };

        var data = await api.V1_Book_Create(model);

        Assert.That(data, Is.Not.Null);
        Assert.That(data.Title, Is.EqualTo(model.Title));
    }

    [Test]
    [TestCaseSource(typeof(BooksTestData), nameof(BooksTestData.InvalidBookNames))]
    public async Task AddBook_ValidRequest_InvalidData_ValidationError(string name)
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

        var model = new Services.Books.CreateModel()
        {
            AuthorId = author.Uid,
            Title = name,
        };

        var exception = Assert.CatchAsync<ApiException>(async () => await api.V1_Book_Create(model));

        exception.Should().NotBeNull();
        exception.StatusCode.Should().Be(HttpStatusCode.BadRequest);

        var validationResults = exception.Content.FromJsonString<ErrorResponse>();

        var errFieldMessage = validationResults.FieldErrors.FirstOrDefault(x => x.FieldName.ToLower() == nameof(model.Title).ToLower());

        var keyWordsFound = errFieldMessage.Message.Contains("is required") || errFieldMessage.Message.Contains("length is");

        Assert.That(keyWordsFound, Is.EqualTo(true));
    }
}