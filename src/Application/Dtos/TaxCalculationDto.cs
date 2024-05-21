namespace Application.Dtos;

public record TaxCalculationDto
{
    public string CityName { get; set; } = null!;

    public string VehicleType { get; set; } = null!;

    public IList<DateTime> PassesDates { get; set; } = null!;
}
