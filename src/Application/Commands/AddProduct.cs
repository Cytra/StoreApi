using Application.Entities;
using Application.Models;
using Application.Ports;
using AutoMapper;
using MediatR;

namespace Application.Commands;

public static class AddProduct
{
    public class Command : Product, IRequest<Response>
    {
    }

    public class Response : BaseResponse
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