using Application.Entities;
using Application.Models;
using Application.Ports;
using AutoMapper;
using MediatR;

namespace Application.Commands;

public static class UpdateProduct
{
    public class Command : Product, IRequest<Response>
    {
        public int Id { get; set; }
    }

    public class Response : BaseResponse
    {
        public bool Updated { get; set; }
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

            _mapper.Map(request,existingEntity);

            await _repo.Update(existingEntity, true);
            return new Response(){ Updated = true};
        }
    }
}