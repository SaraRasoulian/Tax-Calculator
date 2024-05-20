namespace CongestionTaxCalculator.DTOs;

public record TaxCalculationDTO
{
    public string VehicleType { get; set; } = null!;

    public DateTime[] PassagesDate { get; set; } = null!;
}
