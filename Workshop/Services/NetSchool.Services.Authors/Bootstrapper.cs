namespace NetSchool.Services.Authors;

using Microsoft.Extensions.DependencyInjection;

public static class Bootstrapper
{
    public static IServiceCollection AddAuthorService(this IServiceCollection services)
    {
        return services
            .AddSingleton<IAuthorService, AuthorService>();            
    }
}