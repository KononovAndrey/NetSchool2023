using System.Text.Json.Serialization;

namespace NetSchool.Web.Pages.Auth.Models;

public class LoginResult
{
    public bool Successful { get; set; }
    
    [JsonPropertyName("scope")]
    public string Scope { get; set; }

    [JsonPropertyName("access_token")]
    public string AccessToken { get; set; }

    [JsonPropertyName("refresh_token")]
    public string RefreshToken { get; set; }
    
    [JsonPropertyName("expires_in")]
    public int? ExpiresIn { get; set; }

    [JsonPropertyName("token_type")]
    public string TokenType { get; set; }

    [JsonPropertyName("error")]
    public string Error { get; set; }

    [JsonPropertyName("error_description")]
    public string ErrorDescription { get; set; }
}