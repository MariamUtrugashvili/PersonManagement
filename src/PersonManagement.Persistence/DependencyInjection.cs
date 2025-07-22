using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PersonManagement.Domain.Repositories;
using PersonManagement.Persistence.Context;
using PersonManagement.Persistence.Repositories;

namespace PersonManagement.Persistence
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddPersistenceServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<PersonDbContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("PersonDb"));
            });

            services.AddScoped<IPersonRepository, PersonRepository>();

            return services;
        }
    }
}
