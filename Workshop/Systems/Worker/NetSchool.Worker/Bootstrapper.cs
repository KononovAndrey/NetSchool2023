namespace NetSchool.Worker;

using NetSchool.Services.RabbitMq;
using Microsoft.Extensions.DependencyInjection;
using NetSchool.Services.Logger;

public static class Bootstrapper
{
    public static IServiceCollection RegisterAppServices(this IServiceCollection services)
    {
        services
            .AddAppLogger()
            .AddRabbitMq()            
            ;

        services.AddSingleton<ITaskExecutor, TaskExecutor>();

        return services;
    }
}