using Application.Common.Models;
using Application.Common.Persistence;
using Application.Common.Specification;
using Ardalis.Specification;
using Domain.Entities;
using MediatR;

namespace Application.Features.Vendors
{
    public class GetvendorsRequest : IRequest<List<VendorDto>>
    {
    }

    public class GetvendorsRequestHandler : IRequestHandler<GetvendorsRequest, List<VendorDto>>
    {
        private readonly IReadRepository<Vendor> _repository;

        public GetvendorsRequestHandler(IReadRepository<Vendor> repository) => _repository = repository;

        public async Task<List<VendorDto>> Handle(GetvendorsRequest request, CancellationToken cancellationToken)
        {
            var spec = new GetvendorsSpecification(request);
            return await _repository.ListAsync(spec, cancellationToken);
        }
    }

    public class GetvendorsSpecification : EntitiesByBaseFilterSpec<Vendor, VendorDto>
    {
        public GetvendorsSpecification(GetvendorsRequest request)
            : base(request) =>
                Query
                    .OrderBy(e => e.Code);
    }
}
