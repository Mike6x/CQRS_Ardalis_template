using Application.Common.Exceptions;
using Application.Common.Persistence;
using Domain.Entities;
using MediatR;
using Microsoft.Extensions.Localization;

namespace Application.Features.Vendors
{
    public class UpdateVendorRequest : IRequest<int>
    {
        public int Id { get; set; }

        public string Code { get; set; } = default!;
        public string Name { get; set; } = default!;
        public string? Description { get; set; }
        public bool IsActive { get; set; }

        public string? Phone { get; set; }
        public string? Email { get; set; }
        public string? Address { get; set; }
        public string? ContactPerson { get; set; }

        public string? TaxCode { get; set; }
    }

    public class UpdateVendorRequestHandler : IRequestHandler<UpdateVendorRequest, int>
    {
        // Add Domain Events automatically by using IRepository
        private readonly IRepository<Vendor> _repository;
        private readonly IStringLocalizer _t;

        public UpdateVendorRequestHandler(IRepository<Vendor> repository, IStringLocalizer<UpdateVendorRequestHandler> localizer) =>
            (_repository, _t) = (repository, localizer);

        public async Task<int> Handle(UpdateVendorRequest request, CancellationToken cancellationToken)
        {
            var entity = await _repository.GetByIdAsync(request.Id, cancellationToken);

            _ = entity
            ?? throw new NotFoundException(_t["Entity {0} Not Found.", request.Id]);

            entity.Update(
                request.Code,
                request.Name,
                request.Description,
                request.IsActive,
                request.Phone,
                request.Email,
                request.Address,
                request.ContactPerson,
                request.TaxCode);

            await _repository.UpdateAsync(entity, cancellationToken);

            return request.Id;
        }
    }
}