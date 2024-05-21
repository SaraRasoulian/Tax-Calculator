using Domain.Interfaces;
using Infrastructure.DbContexts;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Microsoft.Extensions.DependencyInjection;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        // Setting DBContexts
        var connectionString = configuration.GetConnectionString("DefaultConnection");
        services.AddDbContext<TaxDBContext>(options => options.UseNpgsql(connectionString, o => o.UseNodaTime()));
        services.AddHealthChecks().AddNpgSql(connectionString, "TaxDBContext");

        AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

        services.AddScoped<ICityTaxRuleRepository, CityTaxRuleRepository>();
        services.AddScoped<IHolidayRepository, HolidayRepository>();
        services.AddScoped<ITaxAmountRepository, TaxAmountRepository>();
        services.AddScoped<ITaxExemptVehicleRepository, TaxExemptVehicleRepository>();

        return services;
    }
}
