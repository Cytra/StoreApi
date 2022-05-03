using Application.Entities;
using Application.Models;
using Application.Ports;
using AutoMapper;
using MediatR;

namespace Application.Commands;

public static class DeleteProduct
{
    public class Command : IRequest<Response>
    {
        public int Id { get; set; }
    }

    public class Response : BaseResponse
    {
        public bool Deleted { get; set; }
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
            var existingEntity = await _repo
                .GetById(request.Id);

            existingEntity.Deleted = DateTimeOffset.UtcNow;

            await _repo.Update(existingEntity, true);
            return new Response() { Deleted = true };
        }
    }
}