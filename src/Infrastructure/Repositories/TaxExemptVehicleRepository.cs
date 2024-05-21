using Domain.Interfaces;
using Infrastructure.DbContexts;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class TaxExemptVehicleRepository : ITaxExemptVehicleRepository
{
    private readonly TaxDBContext _dbContext;
    public TaxExemptVehicleRepository(TaxDBContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<bool> IsTaxExemptVehicle(long cityId, string vehicleType)
    {
        return await _dbContext.TaxExemptVehicles.AnyAsync
            (x => x.CityTaxRuleId == cityId & x.VehicleType.ToLower() == vehicleType.ToLower());
    }
}
