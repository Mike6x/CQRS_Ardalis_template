using Application.Common.Persistence;
using Domain.Entities;
using MediatR;
using Microsoft.Extensions.Localization;

namespace Application.Features.Vendors
{
    public class UpdateVendorsRequest : IRequest<int>
    {
        public List<Vendor> Vendors { get; set; } 
    }

    public class UpdateVendorsRequestHandler : IRequestHandler<UpdateVendorsRequest, int>
    {
        // Add Domain Events automatically by using IRepository
        private readonly IRepository<Vendor> _repository;
        private readonly IStringLocalizer _t;

        public UpdateVendorsRequestHandler(IRepository<Vendor> repository, IStringLocalizer<UpdateVendorsRequestHandler> localizer) =>
            (_repository, _t) = (repository, localizer);

        public async Task<int> Handle(UpdateVendorsRequest request, CancellationToken cancellationToken)
        {
            await _repository.AddRangeAsync(request.Vendors, cancellationToken);

            return request.Vendors.Count;
        }
    }

}
