namespace NetSchool.Common.Helpers;

using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;

public static class ValidatorsRegisterHelper
{
    public static void Register(IServiceCollection services)
    {
        var assemblies = AppDomain.CurrentDomain.GetAssemblies()
            .Where(s => s.FullName != null && s.FullName.ToLower().StartsWith("netschool."));

        assemblies.ToList().ForEach(x => { services.AddValidatorsFromAssembly(x, ServiceLifetime.Singleton); });
    }
}