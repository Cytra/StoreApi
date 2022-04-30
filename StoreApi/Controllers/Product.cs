using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using StoreApi.Commands;

namespace StoreApi.Controllers
{
    [ApiController]
    public class Product : ControllerBase
    {
        private readonly IMediator _mediator;
        public Product(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> AddProduct(AddProduct.Commands product, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(product, cancellationToken);
            return Ok(result);
        }
    }
}