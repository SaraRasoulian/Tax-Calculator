using Domain.Common;

namespace Domain.Entities;

public record Holiday : EntityBase
{
    public long CityTaxRuleId { get; set; }

    public CityTaxRule CityTaxRule { get; set; } = null!;

    public DateOnly Date { get; set; }

    public string? Description { get; set; }
}