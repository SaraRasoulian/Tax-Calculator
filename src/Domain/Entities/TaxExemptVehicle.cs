using Domain.Common;

namespace Domain.Entities;

public record TaxExemptVehicle : EntityBase
{
    public long CityTaxRuleId { get; set; }

    public CityTaxRule CityTaxRule { get; set; } = null!;

    public string VehicleType { get; set; } = null!;
}