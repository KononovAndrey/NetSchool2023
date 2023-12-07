namespace NetSchool.Api;

using NetSchool.Context.Seeder;
using NetSchool.Services.Logger;
using NetSchool.Services.Settings;
using NetSchool.Api.Settings;
using NetSchool.Services.Books;
using NetSchool.Services.RabbitMq;
using NetSchool.Services.Actions;

public static class Bootstrapper
{
    public static IServiceCollection RegisterServices (this IServiceCollection service, IConfiguration configuration = null)
    {
        service
            .AddMainSettings()
            .AddLogSettings()
            .AddSwaggerSettings()
            .AddAppLogger()
            .AddDbSeeder()
            .AddApiSpecialSettings()
            .AddBookService()
            .AddRabbitMq()
            .AddActions()
            ;

        return service;
    }
}
