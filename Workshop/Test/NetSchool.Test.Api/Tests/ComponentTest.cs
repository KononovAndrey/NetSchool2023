namespace NetSchool.Test.Api;

using Context;
using DotNet.Testcontainers.Containers;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.Extensions.DependencyInjection;
using NetSchool.Common.Security;
using NetSchool.Context.Entities;
using NetSchool.Services.Settings;
using NUnit.Framework;
using System.Threading.Tasks;

public abstract class ComponentTest
{
    #region Properties

    protected IContainer _dbContainer = null;
    protected ApiAppFactory _apiAppFactory = null;
    protected IdentityAppFactory _identityAppFactory = null;
    protected IDbContextFactory<MainDbContext> _contextFactory = null;
    protected IServiceScope _scope = null;
    protected Configuration _configuration = new Configuration();
    protected MainSettings _mainSettings = null;
    protected SwaggerSettings _swaggerSettings = null;
    protected UserManager<User> _userManager = null;

    protected const string apiClientId = "frontend";
    protected const string apiClientSecret = "A3F0811F2E934C4F1114CB693F7D785E";

    protected const string adminUserName = "admin";
    protected const string adminPassword = "Pass1234";
    protected const string adminEmail = "admin@netschool.test";

    protected const string userUserName = "user";
    protected const string userPassword = "Pass1234";
    protected const string userEmail = "user@netschool.test";

    protected const string guestUserName = "guest";
    protected const string guestPassword = "Pass1234";
    protected const string guestEmail = "guest@netschool.test";

    #endregion

    #region Utilities

    public async Task<Api> CreateApiClient(string scopes = "")
    {
        var clientId = apiClientId;
        var clientSecret = apiClientSecret;

        return await Task.FromResult(new Api(_apiAppFactory.CreateClient, _identityAppFactory.CreateClient, clientId, clientSecret, scopes));
    }

    private async Task<Api> CreateApiClientAndSignIn(string userName, string password, string scopes)
    {
        var api = await CreateApiClient(scopes);
        await api.SignIn(userName, password);

        return api;
    }

    public async Task<Api> CreateApiClientAndSignInAsAdmin()
    {
        return await CreateApiClientAndSignIn(adminUserName, adminPassword, $"{AppScopes.BooksRead} {AppScopes.BooksWrite}");
    }

    public async Task<Api> CreateApiClientAndSignInAsUser()
    {
        return await CreateApiClientAndSignIn(userUserName, userPassword, $"{AppScopes.BooksRead}");
    }

    public async Task<Api> CreateApiClientAndSignInAsGuest()
    {
        return await CreateApiClientAndSignIn(userUserName, userPassword, $"offline_access");
    }

    protected virtual async Task ClearDb()
    {
        using var context = await _contextFactory.CreateDbContextAsync();

        context.Books.RemoveRange(context.Books);
        context.AuthorDetails.RemoveRange(context.AuthorDetails);
        context.Authors.RemoveRange(context.Authors);

        await context.SaveChangesAsync();
    }

    protected HttpClient GetIdentityHttpClient()
    {
        return _identityAppFactory?.CreateClient();
    }

    #endregion

    [OneTimeSetUp]
    public virtual async Task OneTimeSetUp()
    {
        var dockerContainer = new PostgresDockerContainer();
        await dockerContainer.CreateAndStart();
        _dbContainer = dockerContainer.Container;

        _apiAppFactory = new ApiAppFactory(dockerContainer.Host, dockerContainer.Port, dockerContainer.Password, GetIdentityHttpClient);

        _identityAppFactory = new IdentityAppFactory(dockerContainer.Host, dockerContainer.Port, dockerContainer.Password);


        _scope = _apiAppFactory.Services.CreateScope();

        _contextFactory = _scope.ServiceProvider.GetRequiredService<IDbContextFactory<MainDbContext>>();

        _mainSettings = _scope.ServiceProvider.GetRequiredService<MainSettings>();
        _swaggerSettings = _scope.ServiceProvider.GetRequiredService<SwaggerSettings>();

        _userManager = _scope.ServiceProvider.GetRequiredService<UserManager<User>>();

        await UserAccountHelper.CreateUserAccount(_userManager, adminUserName, adminPassword, adminEmail);
        await UserAccountHelper.CreateUserAccount(_userManager, userUserName, userPassword, userEmail);
        await UserAccountHelper.CreateUserAccount(_userManager, guestUserName, guestPassword, guestEmail);
    }

    [SetUp]
    public virtual async Task SetUp()
    {
        await ClearDb();
    }

    [TearDown]
    public virtual async Task TearDown()
    {
        await ClearDb();
    }

    [OneTimeTearDown]
    public virtual async Task OneTimeTearDown()
    {
        _apiAppFactory?.Dispose();
        _identityAppFactory?.Dispose();
        _userManager?.Dispose();
        _scope.Dispose();
        await _dbContainer.DisposeAsync();
    }
}