using EventSiteAPI.Data;
using EventSiteAPI.Data.Repositories;
using EventSiteAPI.Models;
using EventSiteAPI.Services;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Hosting.Internal;

namespace EventSiteAPI.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            return services.AddScoped<CampusRepository>()
                .AddScoped<CitiesRepository>()
                .AddScoped<EventsRepository>()
                .AddScoped<RevellersRepository>();
        }

        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            return services.AddScoped<EventsService>()
                .AddScoped<IdentityService>();
        }

        public static IServiceCollection
            RegisterContext(this IServiceCollection services,
                IConfiguration configuration) //On fait une extension de classe
        {
            var postgresConfig = new PostgresConfiguration();

            configuration.GetSection("PostgreSQL").Bind(postgresConfig);

            var connectionString =
                $"Server={postgresConfig.Server};" +
                $"Port={postgresConfig.Port};" +
                $"User Id={postgresConfig.UserId};" +
                $"Password={postgresConfig.Password};" +
                $"Database={postgresConfig.Database};";

            return services.AddDbContext<ApplicationDbContext>((_, options) =>
            {
                options.UseNpgsql(connectionString,
                    o => o.EnableRetryOnFailure(5));
            });
        }
    }
}