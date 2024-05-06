namespace Tutorial6.Repositories;

public interface IProductRepository
{
    Task<bool> DoesProductExist(int id);
    Task<double> GetPrice(int id);
}