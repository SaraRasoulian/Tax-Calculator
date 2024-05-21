using Application.Dtos;
using Application.Services.Interfaces;
using Domain.Entities;
using Infrastructure.DbContexts;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Services;

// TODO: implement repository design pattern
public class TaxService : ITaxService
{
    private readonly TaxDBContext _dbContext;
    public TaxService(TaxDBContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<long> CalculateTax(TaxCalculationDto dto)
    {
        // TODO: if dates aren't for a single day return -1

        // TODO: if the year is not 2013 return -1

        var city = await GetCityByName(dto.CityName);

        if (city is null)
            return -1;

        if (await IsTaxExemptVehicle(city.Id, dto.VehicleType))
            return 0;

        dto.PassesDates = dto.PassesDates.OrderBy(date => date).ToArray();

        DateOnly passDate = DateOnly.FromDateTime(dto.PassesDates[0]);
        if (await IsTaxExemptDate(city.Id, passDate))
            return 0;

        long totalFee = 0;

        // Single charge rule does not apply
        if (city.SingleChargeDurationMinutes is 0)
        {
            foreach (DateTime date in dto.PassesDates)
            {
                TimeOnly time = TimeOnly.FromDateTime(date);
                totalFee += await GetTaxAmount(city.Id, time);
            }
        }
        // Single charge rule applies
        else
        {
            long currentMaxFee = 0;
            DateTime intervalStart = dto.PassesDates[0];

            foreach (DateTime date in dto.PassesDates)
            {
                TimeOnly time = TimeOnly.FromDateTime(date);
                long nextFee = await GetTaxAmount(city.Id, time);

                TimeSpan span = date.Subtract(intervalStart);
                double minutesDiff = span.TotalMinutes;

                if (minutesDiff <= city.SingleChargeDurationMinutes)
                {

                    if (nextFee > currentMaxFee)
                    {
                        currentMaxFee = nextFee;
                    }
                }
                else
                {
                    totalFee += currentMaxFee;

                    intervalStart = date;
                    currentMaxFee = nextFee;
                }
            }

            totalFee += currentMaxFee;
        }

        if (city.MaximumTaxPerDay != 0 && totalFee > city.MaximumTaxPerDay)
            return city.MaximumTaxPerDay;

        return totalFee;
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

    public async Task<long> GetTaxAmount(long cityId, TimeOnly time)
    {
        var taxAmount = await _dbContext.TaxAmounts.Where
            (x => x.CityTaxRuleId == cityId & x.StartTime <= time && x.EndTime >= time)
            .FirstOrDefaultAsync();

        return taxAmount.Amount;
    }

    public async Task<bool> IsHoliday(long cityId, DateOnly passDate)
    {
        return await _dbContext.Holidays.AnyAsync
            (x => x.CityTaxRuleId == cityId & x.Date == passDate);
    }

    public async Task<bool> IsTaxExemptVehicle(long cityId, string vehicleType)
    {
        return await _dbContext.TaxExemptVehicles.AnyAsync
            (x => x.CityTaxRuleId == cityId & x.VehicleType.ToLower() == vehicleType.ToLower());
    }

    public async Task<bool> IsTaxExemptDate(long cityId, DateOnly date)
    {
        var city = await GetCityById(cityId);

        // Public holidays
        if (city.IsHolidayTaxExempt)
        {
            if (await IsHoliday(cityId, date))
                return true;
        }

        // A day before a public holiday
        if (city.IsDayBeforeHolidayTaxExempt)
        {
            DateOnly tomorrow = date.AddDays(1);
            if (await IsHoliday(cityId, tomorrow))
                return true;
        }

        // During the month of July
        if (city.IsJulyTaxExempt)
        {
            if (date.Month == 7)
                return true;
        }

        // Weekends
        if (city.IsWeekendTaxExempt)
        {
            if (date.DayOfWeek == DayOfWeek.Saturday || date.DayOfWeek == DayOfWeek.Sunday)
            {
                return true;
            }
        }

        return false;
    }
}