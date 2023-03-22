using Application.Common.FileStorage;
using Application.Common.Interfaces;
using Domain.Common;

namespace Application.Common.Exporters;

public interface IExcelReader
{
    Task<IList<T>> ToListAsync<T>(
        FileUploadRequest request,
        FileType supportedFileType,
        string sheetName = "Sheet1");
}
