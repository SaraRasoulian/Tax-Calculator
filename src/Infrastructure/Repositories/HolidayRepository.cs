using Domain.Interfaces;
using Infrastructure.DbContexts;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class HolidayRepository : IHolidayRepository
{
    private readonly TaxDBContext _dbContext;
    public HolidayRepository(TaxDBContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<bool> IsHoliday(long cityId, DateOnly passDate)
    {
        return await _dbContext.Holidays.AnyAsync
            (x => x.CityTaxRuleId == cityId & x.Date == passDate);
    }
}
