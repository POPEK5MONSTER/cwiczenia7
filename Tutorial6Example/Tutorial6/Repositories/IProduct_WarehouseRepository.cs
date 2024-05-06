using Tutorial6.Models;

namespace Tutorial6.Repositories;

public interface IProduct_WarehouseRepository
{
    Task<bool> DoesIdOrderExist(int id);
    Task<int> InsertProduct(Product_Warehouse productWarehouse);
}
