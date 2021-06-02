using EventSiteAPI.Data;
using EventSiteAPI.Data.Repositories;
using EventSiteAPI.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

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
        public static IServiceCollection RegisterContext(this IServiceCollection services, IConfiguration configuration)    //On fait une extension de classe
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