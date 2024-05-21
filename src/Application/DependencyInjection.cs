using Application.Services.Interfaces;
using Application.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Application;
public static class DependencyInjection
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddScoped<ITaxService, TaxService>();

        return services;
    }
}