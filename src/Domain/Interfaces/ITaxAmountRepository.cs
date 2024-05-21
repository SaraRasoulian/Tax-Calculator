namespace Domain.Interfaces;

public interface ITaxAmountRepository
{
    Task<long> GetTaxAmount(long cityId, TimeOnly time);

    // TODO: Implement create, update and delete methods
}
