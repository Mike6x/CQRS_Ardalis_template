using Application.Common.Models;
using Application.Common.Persistence;
using Application.Common.Specification;
using Ardalis.Specification;
using Domain.Entities;
using MediatR;
using Microsoft.Extensions.Localization;

namespace Application.Features.Vendors
{
    public class SearchVendorsRequest : PaginationFilter, IRequest<PaginationResponse<VendorDto>>
    {
    }

    public class SearchVendorsRequestHandler : IRequestHandler<SearchVendorsRequest, PaginationResponse<VendorDto>>
    {
        private readonly IReadRepository<Vendor> _repository;

        public SearchVendorsRequestHandler(IReadRepository<Vendor> repository) => _repository = repository;

        public async Task<PaginationResponse<VendorDto>> Handle(SearchVendorsRequest request, CancellationToken cancellationToken)
        {
            var spec = new SearchVendorsSpecification(request);
            return await _repository.PaginatedListAsync(spec, request.PageNumber, request.PageSize, cancellationToken);
        }
    }

    public class SearchVendorsSpecification : EntitiesByPaginationFilterSpec<Vendor, VendorDto>
    {
        public SearchVendorsSpecification(SearchVendorsRequest request)
            : base(request) =>
                Query
                    .OrderBy(e => e.Code, !request.HasOrderBy());
    }
}
