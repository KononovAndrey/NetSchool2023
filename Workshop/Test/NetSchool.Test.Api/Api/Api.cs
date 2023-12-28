namespace NetSchool.Test.Api;

using Common.Exceptions;
using IdentityModel.Client;
using Refit;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Collections.Generic;
using NetSchool.Services.Books;

public class Api : IDisposable
{
    #region Variables

    private static object _syncLock = new();

    private readonly Func<HttpClient> _createApiHttpClient;
    private readonly Func<HttpClient> _createIdentityHttpClient;
    
    private readonly HttpClient _apiHttpClient;
    private readonly HttpClient _identityHttpClient;

    private readonly string _clientId;
    private readonly string _clientSecret;
    private readonly string _scopes;

    private IApi _api = null;

    private string _userName = string.Empty;
    private string _password = string.Empty;

    private string _accessToken = string.Empty;

    #endregion

    #region System

    public Api(
        Func<HttpClient> createApiHttpClient,
        Func<HttpClient> createIdentityHttpClient,
        string clientId, 
        string clientSecret, 
        string scopes)
    {
        _createApiHttpClient = createApiHttpClient;
        _createIdentityHttpClient = createIdentityHttpClient;
        _clientId = clientId;
        _clientSecret = clientSecret;
        _scopes = scopes;

        _apiHttpClient = _createApiHttpClient.Invoke();

        _identityHttpClient = _createIdentityHttpClient.Invoke();

        _api = RestService.For<IApi>(_apiHttpClient);
    }

    public void Dispose()
    {
        _apiHttpClient.Dispose();
        _identityHttpClient.Dispose();
    }

    private async Task _signInAsUser(string userName, string password)
    {
        try
        {
            var tokenResponse = await _identityHttpClient.RequestPasswordTokenAsync(new PasswordTokenRequest
            {
                Address = $"/connect/token",
                ClientId = $"{_clientId}",
                ClientSecret = $"{_clientSecret}",
                Scope = $"{_scopes}",
                UserName = userName,
                Password = password
            }).ConfigureAwait(true);

            tokenResponse.HttpResponse.EnsureSuccessStatusCode();

            lock (_syncLock)
            {
                _userName = userName;
                _password = password;
                _accessToken = tokenResponse.AccessToken;
            }
        }
        catch (Exception ex)
        {
            throw new ProcessException("ApiClient.SignInAsUser exception", ex);
        }
    }

    public async Task SignIn(string userName, string password)
    {
        if (userName != _userName && password != _password && _accessToken != string.Empty)
            await SignOut();

        await _signInAsUser(userName, password);
    }   
    
    public Task SignOut()
    {
        lock (_syncLock)
        {
            _accessToken = string.Empty;
        }

        return Task.CompletedTask;
    }

    #endregion

    #region Office

    public async Task<IEnumerable<BookModel>> V1_Book_GetAll() =>
        await _api.V1_Book_GetAll(_accessToken);

    public async Task<BookModel> V1_Book_Get(Guid id) =>
        await _api.V1_Book_Get(_accessToken, id);

    public async Task<BookModel> V1_Book_Create(CreateModel request) =>
        await _api.V1_Book_Create(_accessToken, request);

    public async Task V1_Book_Delete(Guid id) =>
        await _api.V1_Book_Delete(_accessToken, id);

    #endregion
}