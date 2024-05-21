using Domain.Common;

namespace Domain.Entities;

public record CityTaxRule : EntityBase
{
    public string CityName { get; set; } = null!;

    public long MaximumTaxPerDay { get; set; }

    public uint SingleChargeDurationMinutes { get; set; }

    public bool IsHolidayTaxExempt { get; set; }

    public bool IsDayBeforeHolidayTaxExempt { get; set; }

    public bool IsWeekendTaxExempt { get; set; }

    public bool IsJulyTaxExempt { get; set; }

    public ICollection<TaxAmount> TaxAmounts { get; } = new List<TaxAmount>();

    public ICollection<Holiday> Holidays { get; } = new List<Holiday>();

    public ICollection<TaxExemptVehicle> TaxExemptVehicles { get; } = new List<TaxExemptVehicle>();
}
