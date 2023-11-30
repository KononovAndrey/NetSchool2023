namespace NetSchool.Api.Configuration;

using Microsoft.Extensions.Diagnostics.HealthChecks;
using System.Reflection;

public class SelfHealthCheck : IHealthCheck
{
    public Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context,
        CancellationToken cancellationToken = default)
    {
        var assembly = Assembly.Load("NetSchool.API");
        var versionNumber = assembly.GetName().Version;

        return Task.FromResult(HealthCheckResult.Healthy($"Build {versionNumber}"));
    }
}