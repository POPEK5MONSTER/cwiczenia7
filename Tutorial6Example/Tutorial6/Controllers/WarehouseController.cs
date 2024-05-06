using System.Transactions;
using Microsoft.AspNetCore.Mvc;
using Tutorial6.Models;
using Tutorial6.Repositories;

namespace Tutorial6.Controllers;

[Route("api/[controller]")]
[ApiController]
public class WarehouseController : ControllerBase
{
    private readonly IProductRepository _productRepository;
    private readonly IWarehouseRepository _warehouseRepository;
    private readonly IOrderRepository _orderRepository;
    private readonly IProduct_WarehouseRepository _productWarehouse;
    public WarehouseController(IProductRepository productRepository, IWarehouseRepository warehouseRepository, IOrderRepository orderRepository, IProduct_WarehouseRepository productWarehouse)
    {
        _productRepository = productRepository;
        _warehouseRepository = warehouseRepository;
        _orderRepository = orderRepository;
        _productWarehouse = productWarehouse;
    }
    
    [HttpPost]
    public async Task<IActionResult> AddRecordToProduct_Warehouse(AddPeoduct_Warehouse addPeoductWarehouse)
    {

        
        if (!await _productRepository.DoesProductExist(addPeoductWarehouse.IdPorduct))
        {
            return NotFound("nie znaleziono produktu");
        }

        if (!await _warehouseRepository.DoesProductExist(addPeoductWarehouse.IdWarehouse))
        {
            return NotFound("nie znaleziono Warehouse");
        }

        var idOrder = await _orderRepository.FindOrder(
            addPeoductWarehouse.IdPorduct, addPeoductWarehouse.Amount, addPeoductWarehouse.CreatedAt);

        if (idOrder == -1)
        {
            return NotFound("błąd");
        }
            
        if (await _productWarehouse.DoesIdOrderExist(idOrder))
        {
            return NotFound("nic");
        }
        if (!await _orderRepository.DoesFulfulledAt(idOrder))
        {
            return NotFound("błądd");
        }

        var price = await _productRepository.GetPrice(addPeoductWarehouse.IdPorduct);
        var newProductWarehouse = new Product_Warehouse
        {
            IdProduct = addPeoductWarehouse.IdPorduct,
            IdWarehouse = addPeoductWarehouse.IdWarehouse,
            IdOreder = idOrder,
            Amount = addPeoductWarehouse.Amount,
            Price = price,
            CreatedAd = addPeoductWarehouse.CreatedAt
        };
        using (TransactionScope scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
        {
            await _orderRepository.UpdateFulfulledAt(idOrder);
            await _productWarehouse.InsertProduct(newProductWarehouse);
            scope.Complete();
        }
        return Ok();
    }
}