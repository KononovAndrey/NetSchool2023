namespace NetSchool.Api.Configuration;

using NetSchool.Services.Settings;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;
using Swashbuckle.AspNetCore.SwaggerGen;
using Swashbuckle.AspNetCore.SwaggerUI;
using System.Reflection;
using Asp.Versioning.ApiExplorer;

/// <summary>
/// Swagger configuration
/// </summary>
public static class SwaggerConfiguration
{
    private static string AppTitle = "DSR NetSchool API";

    /// <summary>
    /// Add OpenAPI to API
    /// </summary>
    /// <param name="services">Services collection</param>
    /// <param name="mainSettings"></param>
    /// <param name="swaggerSettings"></param>
    public static IServiceCollection AddAppSwagger(this IServiceCollection services, MainSettings mainSettings, SwaggerSettings swaggerSettings)
    {
        if (!swaggerSettings.Enabled)
            return services;

        services
            .AddOptions<SwaggerGenOptions>()
            .Configure<IApiVersionDescriptionProvider>((options, provider) =>
            {
                foreach (var avd in provider.ApiVersionDescriptions)
                    options.SwaggerDoc(avd.GroupName, new OpenApiInfo
                    {
                        Version = avd.GroupName,
                        Title = $"{AppTitle}"
                    });
            });

        services.AddSwaggerGen(options =>
        {
            options.SupportNonNullableReferenceTypes();

            options.UseInlineDefinitionsForEnums();

            options.ResolveConflictingActions(apiDescriptions => apiDescriptions.First());

            options.DescribeAllParametersInCamelCase();

            options.CustomSchemaIds(x => x.FullName);

            var xmlPath = Path.Combine(AppContext.BaseDirectory, "api.xml");
            if (File.Exists(xmlPath))
                options.IncludeXmlComments(xmlPath);

            options.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
            {
                Name = "Bearer",
                Type = SecuritySchemeType.OAuth2,
                Scheme = "oauth2",
                BearerFormat = "JWT",
                In = ParameterLocation.Header,
                Flows = new OpenApiOAuthFlows
                {
                    Password = new OpenApiOAuthFlow
                    {
                        TokenUrl = new Uri($"{mainSettings.PublicUrl}/connect/token"),
                        //Scopes = new Dictionary<string, string>
                        //{
                        //    { "Admin", "Admin scope" },
                        //    { "User", "User scope" }
                        //}
                    }
                }
            });


            options.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "oauth2"
                        }
                    },
                    new List<string>()
                }
            });

            options.UseOneOfForPolymorphism();
            options.EnableAnnotations(true, true);

            options.UseAllOfForInheritance();
            options.UseOneOfForPolymorphism();

            options.SelectSubTypesUsing(baseType =>
                typeof(Program).Assembly.GetTypes().Where(type => type.IsSubclassOf(baseType))
            );

            options.ExampleFilters();
        });

        services.AddSwaggerExamplesFromAssemblies(Assembly.GetEntryAssembly());

        services.AddSwaggerGenNewtonsoftSupport();

        return services;
    }


    /// <summary>
    /// Start OpenAPI UI
    /// </summary>
    /// <param name="app">Web application</param>
    public static void UseAppSwagger(this WebApplication app)
    {
        var mainSettings = app.Services.GetService<MainSettings>();
        var swaggerSettings = app.Services.GetService<SwaggerSettings>();

        if (!swaggerSettings?.Enabled ?? false)
            return;

        var provider = app.Services.GetRequiredService<IApiVersionDescriptionProvider>();

        app.UseSwagger(options => { options.RouteTemplate = "docs/{documentname}/api.yaml"; });

        app.UseSwaggerUI(
            options =>
            {
                options.RoutePrefix = "docs";
                provider.ApiVersionDescriptions.ToList().ForEach(
                    description =>
                        options.SwaggerEndpoint(
                            mainSettings.PublicUrl + $"/docs/{description.GroupName}/api.yaml",
                            description.GroupName.ToUpperInvariant())
                );

                options.DocExpansion(DocExpansion.List);
                options.DefaultModelsExpandDepth(-1);
                options.OAuthAppName(AppTitle);

                options.OAuthClientId(swaggerSettings?.OAuthClientId ?? "");
                options.OAuthClientSecret(swaggerSettings?.OAuthClientSecret ?? "");
            }
        );
    }
}