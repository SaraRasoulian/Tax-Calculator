using CongestionTaxCalculator.Services;

namespace UnitTests;

public class TaxServiceTests
{
    private readonly TaxService _taxService;
    public TaxServiceTests()
    {
        _taxService = new TaxService();
    }

    [Theory]
    [InlineData("Car", false)]
    [InlineData("Van", false)]
    [InlineData("Emergency", true)]
    [InlineData("Motorcycle", true)]
    public void IsTollFreeVehicle_Should_Retrun_Boolian(string vehicleType, bool isTollFree)
    {
        // Arrange

        // Act
        bool result = _taxService.IsTollFreeVehicle(vehicleType);

        // Assert
        Assert.Equal(isTollFree, result);
    }

    [Theory]
    [InlineData("2013-05-13", false)]
    [InlineData("2013-05-12", true)] // Weekend
    [InlineData("2013-07-01", true)] // July
    [InlineData("2013-01-01", true)] // Public holiday
    public void IsTollFreeDate_Should_Return_Boolian(string inputDate, bool isTollFree)
    {
        // Arrange
        DateTime date = DateTime.Parse(inputDate);

        // Act
        bool result = _taxService.IsTollFreeDate(date);

        // Assert
        Assert.Equal(isTollFree, result);
    }

}
