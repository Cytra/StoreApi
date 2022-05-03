using System.Net.Mime;
using System.Threading;
using System.Threading.Tasks;
using Application.Commands;
using Application.Models;
using Application.Queries;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace StoreApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Produces(MediaTypeNames.Application.Json)]
    [Consumes(MediaTypeNames.Application.Json)]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorResponse))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ErrorResponse))]
    public class ProductController : ControllerBase
    {
        private readonly IMediator _mediator;
        public ProductController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> AddProduct(
            AddProduct.Command product, 
            CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(product, 
                cancellationToken);
            return Ok(result);
        }

        [HttpPatch]
        public async Task<IActionResult> UpdateProduct(
            UpdateProduct.Command product,
            CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(product,
                cancellationToken);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(
            int id, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(
                new DeleteProduct.Command(){ Id = id},
                cancellationToken);
            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetProduct(
            [FromQuery] GetProducts.Query product,
            CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(
                product,
                cancellationToken);
            return Ok(result);
        }
    }
}