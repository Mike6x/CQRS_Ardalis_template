using Application.Common.Exceptions;
using Application.Common.Persistence;
using Domain.Entities;
using MediatR;
using Microsoft.Extensions.Localization;

namespace Application.Features.Vendors
{
    public class DeleteVendorRequest : IRequest<int>
    {
        public int Id { get; set; }

        public DeleteVendorRequest(int id) => Id = id;
    }

    public class DeleteVendorRequestHandler : IRequestHandler<DeleteVendorRequest, int>
    {
        // Add Domain Events automatically by using IRepository
        private readonly IRepository<Vendor> _repository;

        // private readonly IReadRepository<Asset> _childRepository;
        private readonly IStringLocalizer _t;

        public DeleteVendorRequestHandler(IRepository<Vendor> repository, IStringLocalizer<DeleteVendorRequestHandler> localizer) =>
            (_repository, _t) = (repository, localizer);

        public async Task<int> Handle(DeleteVendorRequest request, CancellationToken cancellationToken)
        {
            // if (await _childRepository.AnyAsync(new EmployeesByVendorSpec(request.Id), cancellationToken))
            // {
            //    throw new ConflictException(_t["Vendor cannot be deleted as it's being used."]);
            // }

            var entity = await _repository.GetByIdAsync(request.Id, cancellationToken);

            _ = entity ?? throw new NotFoundException(_t["Vendor {0} Not Found."]);

            await _repository.DeleteAsync(entity, cancellationToken);

            return request.Id;
        }
    }
}