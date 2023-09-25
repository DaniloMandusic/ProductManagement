using System.ComponentModel.DataAnnotations.Schema;

namespace Infrastructure.Models;

[Table("products")]
public class Product
{
    public int id { get; set; }
    public string name { get; set; }
    public double price { get; set; }
    
    public string description { get; set; }
    public int categoryId { get; set; }
    public int manufacturerId { get; set; }
    public int supplierId { get; set; }
    
}