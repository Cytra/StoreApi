using Application.Models.Enums;

namespace Application.Entities;

public class ProductEntity : EntityBase
{
    public decimal Price { get; set; }
    public decimal Discount { get; set; }
    public int Stock { get; set; }
    public Brand Brand { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public ProductCategory Category { get; set; }
    public ProductSubCategory SubCategory { get; set; }
}