using Application.Dtos;

namespace Application.Services.Interfaces;

public interface ITaxService
{
    /// <summary>
    /// Calculate the tax (total toll fee) for one day
    /// </summary>
    /// <param name="dto"></param>
    /// <returns>tax amount</returns>
    Task<long> CalculateTax(TaxCalculationDto dto);

    Task<bool> IsTaxExemptDate(long cityId, DateOnly date);
}
