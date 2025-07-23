using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.Extensions.DependencyInjection;
using PersonManagement.Application.Persons.Commands.CreatePerson;
using System.Reflection;

namespace PersonManagement.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddFluentValidationAutoValidation()
                    .AddFluentValidationClientsideAdapters();

            services.AddValidatorsFromAssemblyContaining<CreatePersonCommandValidator>();

            services.AddMediatR(cfg =>
                cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));

            return services;
        }
    }
}
