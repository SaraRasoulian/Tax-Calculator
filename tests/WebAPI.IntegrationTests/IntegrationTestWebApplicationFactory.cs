using Infrastructure.DbContexts;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Testcontainers.PostgreSql;

namespace WebAPI.IntegrationTests;

public class IntegrationTestWebApplicationFactory : WebApplicationFactory<Program>, IAsyncLifetime
{
    private readonly PostgreSqlContainer _dbContainer = new PostgreSqlBuilder()
        .WithImage("postgres:latest")
        .WithDatabase("TaxDb_Test")
        .WithUsername("postgres")
        .WithPassword("mysecretpassword")
        .Build();

    public async Task InitializeAsync()
    {
        await _dbContainer.StartAsync();

        using (var scope = Services.CreateScope())
        {
            var scopedServices = scope.ServiceProvider;
            var cntx = scopedServices.GetRequiredService<TaxDBContext>();

            await cntx.Database.EnsureCreatedAsync();
        }
    }

    public new async Task DisposeAsync()
    {
        await _dbContainer.StopAsync();
    }

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureTestServices(services =>
        {
            var descriptor = services.SingleOrDefault(s => s.ServiceType == typeof(DbContextOptions<TaxDBContext>));

            if (descriptor is not null)
            {
                services.Remove(descriptor);
            }

            services.AddDbContext<TaxDBContext>(options =>
            {
                options.UseNpgsql(_dbContainer.GetConnectionString());
            });
        });
    }
}

