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


}
