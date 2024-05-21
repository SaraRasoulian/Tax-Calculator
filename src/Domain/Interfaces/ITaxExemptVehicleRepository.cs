namespace Domain.Interfaces;

public interface ITaxExemptVehicleRepository
{
    Task<bool> IsTaxExemptVehicle(long cityId, string vehicleType);

    // TODO: Implement create, update and delete methods
}
