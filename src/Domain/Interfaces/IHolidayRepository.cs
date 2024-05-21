namespace Domain.Interfaces;

public interface IHolidayRepository
{
    Task<bool> IsHoliday(long cityId, DateOnly passDate);

    // TODO: Implement create, update and delete methods
}
