namespace NetSchool.Api.App;

using Asp.Versioning;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NetSchool.Common.Security;
using NetSchool.Services.Books;
using NetSchool.Services.Logger;

[ApiController]
[Authorize]
[ApiVersion("1.0")]
[ApiExplorerSettings(GroupName = "Product")]
[Route("v{version:apiVersion}/[controller]")]
public class BookController : ControllerBase
{
    private readonly IAppLogger logger;
    private readonly IBookService bookService;

    public BookController(IAppLogger logger, IBookService bookService)
    {
        this.logger = logger;
        this.bookService = bookService;
    }

    [HttpGet("")]
    [Authorize(AppScopes.BooksRead)]
    public async Task<IEnumerable<BookModel>> GetAll()
    {
        var result = await bookService.GetAll();

        return result;
    }

    [HttpGet("{id:Guid}")]
    [Authorize(AppScopes.BooksRead)]
    public async Task<IActionResult> Get([FromRoute] Guid id)
    {
        var result = await bookService.GetById(id);

        if (result == null)
            return NotFound();

        return Ok(result);
    }

    [HttpPost("")]
    [Authorize(AppScopes.BooksWrite)]
    public async Task<BookModel> Create(CreateModel request)
    {
        var result = await bookService.Create(request);

        return result;
    }

    [HttpPut("{id:Guid}")]
    [Authorize(AppScopes.BooksWrite)]
    public async Task Update([FromRoute] Guid id, UpdateModel request)
    {
        await bookService.Update(id, request);
    }

    [HttpDelete("{id:Guid}")]
    [Authorize(AppScopes.BooksWrite)]
    public async Task Delete([FromRoute] Guid id)
    {
        await bookService.Delete(id);
    }

}
