using Microsoft.Extensions.DependencyInjection;
using PasswordValidator.Business.Interfaces;
using PasswordValidator.Business.Services;

namespace PasswordValidator.Api.Configuration
{
    public static class DependencyInjectionConfig
    {
        public static IServiceCollection ResolveDependencies(this IServiceCollection services)
        {
            services.AddScoped<IPasswordValidatorService, PasswordValidatorService>();
            return services;
        }
    }
}
