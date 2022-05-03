using Application.Entities;
using Application.Models.Enums;
using Application.Ports;
using AutoMapper;
using MediatR;

namespace Application.Commands;

public static class AddProduct
{
    public class Command : IRequest<Response>
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

    public class Response
    {

    }

    public sealed class Handler : IRequestHandler<Command, Response>
    {
        private readonly IGenericRepository<ProductEntity> _repo;
        private readonly IMapper _mapper;

        public Handler(IGenericRepository<ProductEntity> repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<Response> Handle(Command request, CancellationToken cancellationToken)
        {
            var entity = _mapper.Map<ProductEntity>(request);
            await _repo.Insert(entity, true);
            return new Response();
        }
    }
}