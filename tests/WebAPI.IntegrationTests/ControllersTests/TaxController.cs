using Application.Dtos;
using System.Net.Http.Json;
using FluentAssertions;
using System.Net;

namespace WebAPI.IntegrationTests.ControllersTests;

public class TaxController : BaseControllerTest
{
    public TaxController(IntegrationTestWebApplicationFactory factory) : base(factory)
    {
    }

    [Fact]
    public async Task CalculateTax_WithValidInput_Returns_OKWithTaxAmount()
    {
        // Arrange
        TaxCalculationDto dto = new TaxCalculationDto
        {
            CityName = "Gothenburg",
            VehicleType = "Car",
            PassesDates = new List<DateTime>
                {
                    new DateTime(2013, 5, 13, 6, 15, 0),
                    new DateTime(2013, 5, 13, 8, 45, 0),
                    new DateTime(2013, 5, 13, 15, 30, 0),
                }
        };

        // Act
        var response = await _httpClient.PostAsJsonAsync("api/tax/calculate", dto);

        // Assert
        response.Should().HaveStatusCode(HttpStatusCode.OK);
        var tax = long.Parse(await response.Content.ReadAsStringAsync());
        tax.Should().BeGreaterThanOrEqualTo(0);
    }

    [Fact]
    public async Task CalculateTax_WithMissingCityName_Returns_BadRequest()
    {
        // Arrange
        TaxCalculationDto dto = new TaxCalculationDto
        {
            CityName = null,
            VehicleType = "Buss",
            PassesDates = new List<DateTime>
                {
                    new DateTime(2013, 12, 1, 12, 15, 0),
                }
        };

        // Act
        var response = await _httpClient.PostAsJsonAsync("api/tax/calculate", dto);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }
}
