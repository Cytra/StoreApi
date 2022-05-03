using Application.Commands;
using Application.Models.Enums;
using Swashbuckle.AspNetCore.Filters;

namespace StoreApi.Samples;

public class AddProductSample : IExamplesProvider<AddProduct.Command>
{
    public AddProduct.Command GetExamples()
    {
        return new AddProduct.Command()
        {
            Brand = Brand.CkTools,
            Category = ProductCategory.Tools,
            SubCategory = ProductSubCategory.Pliers,
            Description = "Suktukas",
            Discount = 0,
            Name = "Suktukas",
            Price = 20,
            Stock = 10
        };
    }
}