namespace NetSchool.Services.Settings;

public class SwaggerSettings
{
    public bool Enabled { get; private set; } = false;

    public string OAuthClientId { get; private set; }
    public string OAuthClientSecret { get; private set; }

    public SwaggerSettings()
    {
        Enabled = false;
    }
}