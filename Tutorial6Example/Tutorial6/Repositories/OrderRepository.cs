using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.Data.SqlClient;

namespace Tutorial6.Repositories;

public class OrderRepository : IOrderRepository
{
    private readonly IConfiguration _configuration;
    public OrderRepository(IConfiguration configuration)
    {
        _configuration = configuration;
    }
    
    public async Task<int> FindOrder(int id, int amount, DateTime dateTime)
    {
        var query = "SELECT TOP 1 IdOrder FROM [Order] WHERE IdProduct = @IdProduct AND Amount = @Amount AND CreatedAt < @CreatedAt";
        
        using SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("Default"));
        using SqlCommand command = new SqlCommand();

        command.Connection = connection;
        command.CommandText = query;
        command.Parameters.AddWithValue("@IdProduct", id);
        command.Parameters.AddWithValue("@Amount", amount);
        command.Parameters.AddWithValue("@CreatedAt", dateTime);

        await connection.OpenAsync();

        var result = await command.ExecuteScalarAsync();
        if (result is not null)
        {
            return (int)result;
        }

        return -1;
    }

    public async Task<bool> DoesFulfulledAt(int id)
    {
        var query = "SELECT 1 FROM [Order] WHERE IdOreder = @IdOreder AND FulfilledAt IS NOT NULL";
        
        using SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("Default"));
        using SqlCommand command = new SqlCommand();

        command.Connection = connection;
        command.CommandText = query;
        command.Parameters.AddWithValue("@IdOreder", id);

        await connection.OpenAsync();

        var res = await command.ExecuteScalarAsync();
        
        return res is not null;
    }

    public async Task<int> UpdateFulfulledAt(int id)
    {
        var query = "UPDATE [Order] SET FulfilledAt = @DatetimeNow WHERE IdOreder = @IdOreder";
        
        using SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("Default"));
        using SqlCommand command = new SqlCommand();

        command.Connection = connection;
        command.CommandText = query;
        command.Parameters.AddWithValue("@DatetimeNow", DateTime.Now);
        command.Parameters.AddWithValue("@IdOreder", id);
        
        await connection.OpenAsync();

        var res = await command.ExecuteScalarAsync();

        if (res is not null)
        {
            return (int)res;
        }

        return -1;

    }
}