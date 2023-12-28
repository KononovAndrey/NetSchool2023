namespace NetSchool.Test.Api;

using DotNet.Testcontainers.Builders;
using DotNet.Testcontainers.Containers;
using System;

public class PostgresDockerContainer
{
    private const string postgresPwd = "pgpwd";
    private const ushort postgresPort = 5432;

    private IContainer container = null;

    public IContainer Container => container;

    public string Host => container?.Hostname;

    public ushort Port => container?.GetMappedPublicPort(postgresPort) ?? 0;

    public string Password => postgresPwd;

    public async Task CreateAndStart()
    {
        if (container != null)
            throw new InvalidOperationException("Container already created");

        container = new ContainerBuilder()
            .WithName(Guid.NewGuid().ToString("N"))
            .WithImage("postgres:15")
            .WithHostname(Guid.NewGuid().ToString("N"))
            .WithExposedPort(postgresPort)
            .WithPortBinding(postgresPort, true)
            .WithEnvironment("POSTGRES_PASSWORD", postgresPwd)
            .WithEnvironment("PGDATA", "/pgdata")
            .WithTmpfsMount("/pgdata")
            .WithWaitStrategy(Wait.ForUnixContainer().UntilCommandIsCompleted("psql -U postgres -c \"select 1\""))
            .Build();

        await container.StartAsync();
    }

    public async void Dispose()
    {
        if (container == null)
            return;

        await container.DisposeAsync();
        container = null;
    }
}