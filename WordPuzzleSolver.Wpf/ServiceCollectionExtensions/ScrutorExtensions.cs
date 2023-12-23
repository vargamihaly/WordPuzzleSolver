using WordPuzzleSolver.Common.Core;
using WordPuzzleSolver.Common.Core.Reflection;
using Microsoft.Extensions.DependencyInjection;

namespace WordPuzzleSolver.Wpf.ServiceCollectionExtensions;

public static class ScrutorExtensions
{
    public static ServiceCollection ConfigureScrutor(this ServiceCollection services)
    {
        // more Scrutor examples and scenarios:
        // https://andrewlock.net/using-scrutor-to-automatically-register-your-services-with-the-asp-net-core-di-container/
        // Scrutpr Github repo:
        // https://github.com/khellang/Scrutor

        services.Scan(scan => scan
            .FromAssemblies(AssemblyScanner.GetAssemblies())
            .AddClasses(classes => classes.AssignableTo<ITransient>())
            .AsImplementedInterfaces()
            .AsSelfWithInterfaces()
            .WithTransientLifetime()
            .AddClasses(classes => classes.AssignableTo<IScoped>())
            .AsImplementedInterfaces()
            .AsSelfWithInterfaces()
            .WithScopedLifetime()
            .AddClasses(classes => classes.AssignableTo<ISingleton>())
            .AsImplementedInterfaces()
            .AsSelfWithInterfaces()
            .WithSingletonLifetime());

        return services;
    }
}