using Application.Common.Persistence;
using Domain.Entities;
using MediatR;
using Microsoft.Extensions.Localization;

namespace Application.Features.Vendors
{
    public class CreateVendorRequest : IRequest<int>
    {
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

    public class CreateVendorRequestHandler : IRequestHandler<CreateVendorRequest, int>
    {
        // Add Domain Events automatically by using IRepository
        private readonly IRepository<Vendor> _repository;

        public CreateVendorRequestHandler(IRepository<Vendor> repository) => _repository = repository;

        public async Task<int> Handle(CreateVendorRequest request, CancellationToken cancellationToken)
        {
            var entity = new Vendor(
                    request.Code,
                    request.Name,
                    request.Description ?? string.Empty,
                    request.IsActive,
                    request.Phone ?? string.Empty,
                    request.Email ?? string.Empty,
                    request.Address ?? string.Empty,
                    request.ContactPerson ?? string.Empty,
                    request.TaxCode ?? string.Empty);

            await _repository.AddAsync(entity, cancellationToken);

            return entity.Id;
        }
    }
}
