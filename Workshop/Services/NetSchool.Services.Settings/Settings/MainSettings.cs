namespace NetSchool.Services.Settings;

public class MainSettings
{
    public string PublicUrl { get; private set; }
    public string InternalHostUrl { get; private set; }
    public string AllowedOrigins { get; private set; }
    public int UploadFileSizeLimit { get; private set; } = 20971520;
}