namespace Tutorial6.Models;

public class Product_Warehouse
{
    
    public int IdProductWarehouse { get; set;}
    public int IdWarehouse { get; set;}
    public int IdProduct { get; set;}
    public int IdOreder { get; set;}
    public int Amount { get; set;}
    public double Price { get; set;}
    public DateTime CreatedAd { get; set;}
}