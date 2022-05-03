using Application.Models.Enums;

namespace Application.Models;

public class Product
{
    public decimal Price { get; set; }
    public decimal Discount { get; set; }
    public int Stock { get; set; }
    public Brand Brand { get; set; }
    public string Name { get; set; } = null!;
    public string Description { get; set; } = null!;
    public ProductCategory Category { get; set; }
    public ProductSubCategory SubCategory { get; set; }
}