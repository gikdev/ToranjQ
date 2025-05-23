using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using ToranjQ.App.Database;
using ToranjQ.App.Repositories;
using ToranjQ.App.Services;

namespace ToranjQ.App;

public static class AppServiceCollectionExtensions
{
    public static IServiceCollection AddApp(this IServiceCollection services)
    {
        services.AddSingleton<IAnswerRepo, AnswerRepo>();
        services.AddSingleton<IAnswerService, AnswerService>();
        services.AddValidatorsFromAssemblyContaining<IApplicationMarker>(ServiceLifetime.Singleton);
        return services;
    }

    public static IServiceCollection AddDb(this IServiceCollection services, string connectionString)
    {
        services.AddSingleton<IDbConnectionFactory>(_ => new NpgsqlConnectionFactory(connectionString));
        services.AddSingleton<DbInitializer>();
        return services;
    }
}