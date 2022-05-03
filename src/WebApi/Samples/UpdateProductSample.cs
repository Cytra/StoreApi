using Application.Commands;
using Application.Models.Enums;
using Swashbuckle.AspNetCore.Filters;

namespace StoreApi.Samples;

public sealed class UpdateProductSample : IExamplesProvider<UpdateProduct.Command>
{
    public UpdateProduct.Command GetExamples()
    {
        return new UpdateProduct.Command()
        {
            Id = 1,
            Brand = Brand.CkTools,
            Category = ProductCategory.Tools,
            SubCategory = ProductSubCategory.Pliers,
            Description = "Reples",
            Discount = 1,
            Name = "Reples",
            Price = 2,
            Stock = 1
        };
    }
}