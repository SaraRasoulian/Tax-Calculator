using Domain.Interfaces;
using Infrastructure.DbContexts;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class TaxAmountRepository : ITaxAmountRepository
{
    private readonly TaxDBContext _dbContext;
    public TaxAmountRepository(TaxDBContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<long> GetTaxAmount(long cityId, TimeOnly time)
    {
        var taxAmount = await _dbContext.TaxAmounts.Where
            (x => x.CityTaxRuleId == cityId & x.StartTime <= time && x.EndTime >= time)
            .FirstOrDefaultAsync();

        return taxAmount.Amount;
    }
}
