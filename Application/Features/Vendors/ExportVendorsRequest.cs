using Application.Common.Exporters;
using Application.Common.Models;
using Application.Common.Persistence;
using Application.Common.Specification;
using Domain.Entities;
using MediatR;
using Microsoft.Extensions.Localization;

namespace Application.Features.Vendors
{
    public class ExportVendorsRequest : BaseFilter, IRequest<Stream>
    {
    }

    public class ExportVendorsRequestHandler : IRequestHandler<ExportVendorsRequest, Stream>
    {
        private readonly IReadRepository<Vendor> _repository;
        private readonly IExcelWriter _excelWriter;

        public ExportVendorsRequestHandler(IReadRepository<Vendor> repository, IExcelWriter excelWriter)
        {
            _repository = repository;
            _excelWriter = excelWriter;
        }

        public async Task<Stream> Handle(ExportVendorsRequest request, CancellationToken cancellationToken)
        {
            var spec = new ExportVendorsSpecification(request);

            var list = await _repository.ListAsync(spec, cancellationToken);

            return _excelWriter.WriteToStream(list);
        }
    }

    public class ExportVendorsSpecification : EntitiesByBaseFilterSpec<Vendor, VendorExportDto>
    {
        public ExportVendorsSpecification(ExportVendorsRequest request)
            : base(request) =>
            Query
               .SearchBy(request);

    }
}