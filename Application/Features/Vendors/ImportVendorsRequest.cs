using Application.Common.Exceptions;
using Application.Common.Exporters;
using Application.Common.FileStorage;
using Application.Common.Persistence;
using Domain.Common;
using Domain.Entities;
using MediatR;
using Microsoft.Extensions.Localization;

namespace Application.Features.Vendors
{
    public class ImportVendorsRequest : IRequest<int>
    {
        public FileUploadRequest ExcelFile { get; set; } = default!;
    }

    public class ImportVendorsRequestHandler : IRequestHandler<ImportVendorsRequest, int>
    {
        private readonly IRepository<Vendor> _repository;
        private readonly IExcelReader _excelReader;
        private readonly IStringLocalizer _localizer;
        public ImportVendorsRequestHandler(
            IRepository<Vendor> repository,
            IExcelReader excelReader,
            IStringLocalizer<ImportVendorsRequestHandler> localizer)
        {
            _repository = repository;
            _excelReader = excelReader;
            _localizer = localizer;
        }

        public async Task<int> Handle(ImportVendorsRequest request, CancellationToken cancellationToken)
        {
            var items = await _excelReader.ToListAsync<Vendor>(request.ExcelFile, FileType.Excel);
            bool errorsOccurred = false;
            var errors = new List<string>();
            int count = 0;

            if (items?.Count > 0)
            {
                foreach (var item in items)
                {
                    if (item.Id == 0)
                    {
                        count++;
                        var response = await _repository.AddAsync(item, cancellationToken);
                        if (response != null)
                        {
                            count++;
                        }
                        else
                        {
                            errorsOccurred = true;
                            errors.Add(_localizer[string.Format("Error happened when importing Item named {0}", item.Name)]);
                        }
                    }
                    else
                    {
                        var response = await _repository.GetByIdAsync(item.Id, cancellationToken);
                        if (response != null)
                        {
                            response.Update(
                                item.Code,
                                item.Name,
                                item.Description,
                                item.IsActive,
                                item.Phone,
                                item.Email,
                                item.Address,
                                item.ContactPerson,
                                item.TaxCode);

                            await _repository.UpdateAsync(response, cancellationToken);
                            count++;
                        }
                        else
                        {
                            errorsOccurred = true;
                            errors.Add(_localizer[string.Format("Item named {0} Not Found", item.Name)]);
                        }
                    }
                }

                if (errorsOccurred)
                {
                    throw new InternalServerException(_localizer["Internal error:"], errors);
                }
            }

            // else
            // {
            //    throw new InvalidOperationException(_localizer["An Error has occurred when uploading!"]);
            // }
            return count;
        }
    }
}
