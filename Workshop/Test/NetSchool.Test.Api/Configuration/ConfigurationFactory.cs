namespace NetSchool.Test.Api;

using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.IO;

public static class ConfigurationFactory
{
    public static IConfiguration Create(IEnumerable<KeyValuePair<string, string>> overriddenVariables = null)
    {
        var configuration = new Configuration();

        if (overriddenVariables != null)
            foreach (var variable in overriddenVariables)
                configuration.SetVariable(variable.Key, variable.Value);

        return new ConfigurationBuilder()
            .AddInMemoryCollection(configuration.Variables!)
            .Build();
    }
}