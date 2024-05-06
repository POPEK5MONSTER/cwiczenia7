namespace Tutorial6.Repositories;

public interface IOrderRepository
{
    Task<int> FindOrder(int id, int amount, DateTime dateTime);
    Task<bool> DoesFulfulledAt(int id);
    Task<int> UpdateFulfulledAt(int id);
}