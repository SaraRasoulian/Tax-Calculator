using Domain.Common;

namespace Domain.Entities;

public record TaxAmount : EntityBase
{
    public long CityTaxRuleId { get; set; }

    public CityTaxRule CityTaxRule { get; set; } = null!;

    public TimeOnly StartTime { get; set; }

    public TimeOnly EndTime { get; set; }

    public long Amount { get; set; }
}
