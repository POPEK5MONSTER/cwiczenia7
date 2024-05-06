using Microsoft.Data.SqlClient;
using Tutorial6.Models;

namespace Tutorial6.Repositories;

public class ProductWarehouseRepositoryRepository : IProduct_WarehouseRepository
{
    private readonly IConfiguration _configuration;

    public ProductWarehouseRepositoryRepository(IConfiguration configuration)
    {
        _configuration = configuration;
    }
    
    public async Task<bool> DoesIdOrderExist(int id)
    {
        var query = "SELECT 1 FROM Product_Warehouse WHERE IdOreder = @IdOreder";
        
        using SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("Default"));
        using SqlCommand command = new SqlCommand();

        command.Connection = connection;
        command.CommandText = query;
        command.Parameters.AddWithValue("@IdOreder", id);

        await connection.OpenAsync();

        var res = await command.ExecuteScalarAsync();
        
        return res is not null;
    }

    public async Task<int> InsertProduct(Product_Warehouse productWarehouse)
    {
        
        var query = "INSERT INTO Product_Warehouse VALUES(@IdWarehouse, @IdProduct, @IdOrder, @Amount, @Price, @CreatedAt)";

        await using SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("Default"));
        await using SqlCommand command = new SqlCommand();
        command.Connection = connection;
        command.CommandText = query;
        command.Parameters.AddWithValue("@IdWarehouse", productWarehouse.IdWarehouse);
        command.Parameters.AddWithValue("@IdProduct", productWarehouse.IdProduct);
        command.Parameters.AddWithValue("@IdOrder", productWarehouse.IdOreder);
        command.Parameters.AddWithValue("@Amount", productWarehouse.Amount);
        command.Parameters.AddWithValue("@Price", productWarehouse.Amount * productWarehouse.Price);
        command.Parameters.AddWithValue("@CreatedAt", DateTime.Now);

        await connection.OpenAsync();
        var res = await command.ExecuteNonQueryAsync();
        var idProduckt = Convert.ToInt32(res);
        return idProduckt;
    }
    
}