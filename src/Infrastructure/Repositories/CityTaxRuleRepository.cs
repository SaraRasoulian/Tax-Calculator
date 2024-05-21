using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.DbContexts;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class CityTaxRuleRepository : ICityTaxRuleRepository
{
    private readonly TaxDBContext _dbContext;
    public CityTaxRuleRepository(TaxDBContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<CityTaxRule?> GetCityByName(string cityName)
    {
        return await _dbContext.CityTaxRules.Where(x => x.CityName.ToLower() == cityName.ToLower())
            .FirstOrDefaultAsync();
    }

    public async Task<CityTaxRule?> GetCityById(long cityId)
    {
        return await _dbContext.CityTaxRules.FindAsync(cityId);
    }
}
