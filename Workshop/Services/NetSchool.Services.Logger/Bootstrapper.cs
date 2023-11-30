namespace NetSchool.Services.Logger;

using Microsoft.Extensions.DependencyInjection;

public static class Bootstrapper
{
    public static IServiceCollection AddAppLogger(this IServiceCollection services)
    {
        return services
            .AddSingleton<IAppLogger, AppLogger>();
    }
}