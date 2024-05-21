using Application.Dtos;
using Domain.Entities;

namespace Application.Services.Interfaces;

public interface ITaxService
{
    /// <summary>
    /// Calculate the total tax (toll fee) for one day
    /// </summary>
    /// <param name="dto"></param>
    /// <returns>tax amount</returns>
    Task<long> CalculateTax(TaxCalculationDto dto);

    Task<CityTaxRule?> GetCityByName(string cityName);

    Task<CityTaxRule?> GetCityById(long cityId);

    Task<long> GetTaxAmount(long cityId, TimeOnly time);

    Task<bool> IsHoliday(long cityId, DateOnly passDate);

    Task<bool> IsTaxExemptVehicle(long cityId, string vehicleType);

    Task<bool> IsTaxExemptDate(long cityId, DateOnly date);
}
