namespace NetSchool.Api;

using NetSchool.Services.Logger;
using NetSchool.Services.Settings;

public static class Bootstrapper
{
    public static IServiceCollection RegisterServices (this IServiceCollection service, IConfiguration configuration = null)
    {
        service
            .AddMainSettings()
            .AddLogSettings()
            .AddSwaggerSettings()
            .AddAppLogger();

        return service;
    }
}
