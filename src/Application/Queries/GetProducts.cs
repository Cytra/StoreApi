using Application.Entities;
using Application.Models.Pagination;
using Application.Ports;
using MediatR;

namespace Application.Queries;

public static class GetProducts
{
    public class Query : IRequest<PagedList<ProductEntity>>
    {
        public int Page { get; set; }
        public int RowsPerPage { get; set; }
    }

    public sealed class Handler : IRequestHandler<Query, PagedList<ProductEntity>>
    {
        private readonly IGenericRepository<ProductEntity> _repo;

        public Handler(IGenericRepository<ProductEntity> repo)
        {
            _repo = repo;
        }

        public async Task<PagedList<ProductEntity>> Handle(Query request, CancellationToken cancellationToken)
        {
            var result = await _repo
                .GetPaged(request.Page, request.RowsPerPage);
            return result;
        }
    }
}