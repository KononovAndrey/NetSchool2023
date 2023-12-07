namespace NetSchool.Services.Books;

using Microsoft.Extensions.DependencyInjection;

public static class Bootstrapper
{
    public static IServiceCollection AddBookService(this IServiceCollection services)
    {
        return services
            .AddSingleton<IBookService, BookService>();            
    }
}