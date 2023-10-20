using Microsoft.EntityFrameworkCore;
using TestAPI.Data;
using TestAPI.Interfaces;
using TestAPI.Services;

namespace TestAPI.Extensions
{
    public static class ApplicationServicesExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services,
        IConfiguration configuration)
        {
            services.AddDbContext<DataContext>(opt =>
            {
                opt.UseSqlite(configuration.GetConnectionString("DefaultConnection"));
            });
            services.AddCors();

            services.AddScoped<ITokenService,TokenService>();
            
            return services;
        }
    }
}