namespace NetSchool.Api.App;

using Asp.Versioning;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NetSchool.Common.Security;
using NetSchool.Services.Authors;
using NetSchool.Services.Logger;

[ApiController]
[Authorize]
[ApiVersion("1.0")]
[ApiExplorerSettings(GroupName = "Product")]
[Route("v{version:apiVersion}/[controller]")]
public class AuthorController : ControllerBase
{
    private readonly IAppLogger logger;
    private readonly IAuthorService authorService;

    public AuthorController(IAppLogger logger, IAuthorService authorService)
    {
        this.logger = logger;
        this.authorService = authorService;
    }

    [HttpGet("")]
    [Authorize(AppScopes.BooksRead)]
    public async Task<IEnumerable<AuthorModel>> GetAll()
    {
        var result = await authorService.GetAll();

        return result;
    }
}
