namespace NetSchool.Test.Api;

using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using NetSchool.Context;
using NetSchool.Services.Actions;
using NetSchool.Services.Authors;
using NetSchool.Services.Books;
using NetSchool.Services.RabbitMq;
using NetSchool.Services.Settings;
using NetSchool.Services.UserAccount;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;

public class ApiAppFactory : WebApplicationFactory<NetSchool.Api.Program>
{
    private readonly string _dbConnStr;
    private readonly Func<HttpClient> _getBackchannelClient;

    public ApiAppFactory(string host, int port, string password, Func<HttpClient> getBackchannelClient)
    {
        var sb = new NpgsqlConnectionStringBuilder
        {
            Host = host,
            Port = port,
            Database = "test_database",
            Username = "postgres",
            Password = password
        };
        _dbConnStr = sb.ConnectionString;

        _getBackchannelClient = getBackchannelClient;
    }

    private void RemoveService(IServiceCollection services, Type serviceType)
    {
        var descriptor = services.SingleOrDefault(d => d.ServiceType == serviceType);
        if (descriptor != null)
            services.Remove(descriptor);
    }

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        // Attention!
        // This lines are required for load to assembly Books, Authors and UserAccounts services
        // Don't remove it
        var book = new BookModel();
        var author = new AuthorModel();
        var user = new UserAccountModel();

        // Configure test environment
        builder.ConfigureTestServices(services =>
        {
            // Apply actual DB settings
            var overriddenVariables = new List<KeyValuePair<string, string>>()
            {
                new("Database:Type", "PgSql"),
                new("Database:ConnectionString", _dbConnStr)
            };

            // Create test configuration
            var testConfiguration = ConfigurationFactory.Create(overriddenVariables);

            // Delete old API work settings
            RemoveService(services, typeof(IConfiguration));
            RemoveService(services, typeof(DbContextOptions<MainDbContext>));
            RemoveService(services, typeof(DbSettings));
            RemoveService(services, typeof(MainSettings));
            RemoveService(services, typeof(SwaggerSettings));
            RemoveService(services, typeof(IAction));
            RemoveService(services, typeof(IRabbitMq));

            // Register new services for tests
            services.AddSingleton(testConfiguration);
            services.AddAppDbContext(testConfiguration);
            services.AddMainSettings(testConfiguration);
            services.AddSwaggerSettings(testConfiguration);

            services.AddSingleton(new Mock<IAction>().Object);
            services.AddSingleton(new Mock<IRabbitMq>().Object);

            services.Configure<JwtBearerOptions>(JwtBearerDefaults.AuthenticationScheme,
                options =>
                {
                    var client = _getBackchannelClient.Invoke();
                    options.Backchannel = client;
                });
        });
    }
}