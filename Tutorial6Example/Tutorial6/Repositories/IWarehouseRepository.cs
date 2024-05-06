namespace Tutorial6.Repositories;

public interface IWarehouseRepository
{
    Task<bool> DoesProductExist(int id);

}