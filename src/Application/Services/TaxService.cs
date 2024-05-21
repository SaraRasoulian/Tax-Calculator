using Application.Dtos;
using Application.Services.Interfaces;
using Domain.Interfaces;

namespace Application.Services;

public class TaxService : ITaxService
{
    private readonly ICityTaxRuleRepository _cityTaxRuleRepository;
    private readonly IHolidayRepository _holidayRepository;
    private readonly ITaxAmountRepository _taxAmountRepository;
    private readonly ITaxExemptVehicleRepository _taxExemptVehicleRepository;

    public TaxService(ICityTaxRuleRepository cityTaxRuleRepository,
        IHolidayRepository holidayRepository,
        ITaxAmountRepository taxAmountRepository,
        ITaxExemptVehicleRepository taxExemptVehicleRepository)
    {
        _cityTaxRuleRepository = cityTaxRuleRepository;
        _holidayRepository = holidayRepository; 
        _taxAmountRepository = taxAmountRepository;
        _taxExemptVehicleRepository = taxExemptVehicleRepository;
    }

    public async Task<long> CalculateTax(TaxCalculationDto dto)
    {
        // TODO: if dates aren't for a single day return -1

        // TODO: if the year is not 2013 return -1

        var city = await _cityTaxRuleRepository.GetCityByName(dto.CityName);

        if (city is null)
            return -1;

        if (await _taxExemptVehicleRepository.IsTaxExemptVehicle(city.Id, dto.VehicleType))
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
                totalFee += await _taxAmountRepository.GetTaxAmount(city.Id, time);
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
                long nextFee = await _taxAmountRepository.GetTaxAmount(city.Id, time);

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

    public async Task<bool> IsTaxExemptDate(long cityId, DateOnly date)
    {
        var city = await _cityTaxRuleRepository.GetCityById(cityId);

        // Public holidays
        if (city.IsHolidayTaxExempt)
        {
            if (await _holidayRepository.IsHoliday(cityId, date))
                return true;
        }

        // A day before a public holiday
        if (city.IsDayBeforeHolidayTaxExempt)
        {
            DateOnly tomorrow = date.AddDays(1);
            if (await _holidayRepository.IsHoliday(cityId, tomorrow))
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