using CongestionTaxCalculator.DTOs;

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

    [Theory]
    [InlineData("06:15:00", 8)]
    [InlineData("06:45:00", 13)]
    [InlineData("07:30:00", 18)]
    [InlineData("08:10:00", 13)]
    [InlineData("10:00:00", 8)]
    [InlineData("15:10:00", 13)]
    [InlineData("16:10:00", 18)]
    [InlineData("17:20:00", 13)]
    [InlineData("18:10:00", 8)]
    [InlineData("19:00:00", 0)]
    public void GetTollFee_Should_Return_TaxAmount(string inputTime, int expectedAmount)
    {
        // Arrange
        TimeOnly time = TimeOnly.Parse(inputTime);

        // Act
        int result = _taxService.GetTollFee(time.Hour, time.Minute);

        // Assert
        Assert.Equal(expectedAmount, result);
    }

    [Theory]
    [InlineData("Car", new string[]
    {
        "2013-05-13T06:15:00",
        "2013-05-13T08:45:00"
    }, 16)]
    [InlineData("Buss", new string[]
    {
        "2013-07-01T21:00:00",
        "2013-07-01T22:00:00"
    }, 0)]
    [InlineData("Car", new string[]
    {
        "2013-01-14T21:00:00"
    }, 0)]
    [InlineData("Emergency", new string[]
    {
        "2013-02-07T06:23:27",
        "2013-02-07T15:27:00"
    }, 0)]
    [InlineData("Car", new string[]
    {
        "2013-02-07T06:23:27",
        "2013-02-07T15:27:00"
    }, 21)]
    [InlineData("Car", new string[]
    {
        "2013-02-08T06:27:00",
        "2013-02-08T06:20:27",
        "2013-02-08T14:35:00",
        "2013-02-08T15:29:00",
        "2013-02-08T15:47:00",
        "2013-02-08T16:01:00",
        "2013-02-08T16:48:00",
        "2013-02-08T17:49:00",
        "2013-02-08T18:29:00",
        "2013-02-08T18:35:00"
    }, 60)]
    public void CalculateTax_Should_Return_TaxAmount(string vehicleType, string[] datetimeStrings, int expectedAmount)
    {
        // Arrange
        TaxService service = new TaxService();
        DateTime[] passagesDate = datetimeStrings.Select(dateString => DateTime.Parse(dateString)).ToArray();
        TaxCalculationDTO dto = new TaxCalculationDTO
        {
            VehicleType = vehicleType,
            PassesDates = passagesDate
        };

        // Act
        int result = service.CalculateTax(dto);

        // Assert
        Assert.Equal(expectedAmount, result);
    }

}
