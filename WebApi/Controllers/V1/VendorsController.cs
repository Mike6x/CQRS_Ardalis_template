using Application.Common.Exporters;
using Application.Common.Models;
using Application.Features.Vendors;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics.Metrics;

namespace WebApi.Controllers.V1
{
    [ApiVersion("1.0")]
    public class VendorsController : BaseApiController
    {
        private readonly IExcelReader _excelReader;
        public VendorsController(IExcelReader excelReader)
        {
            _excelReader = excelReader;
        }
        
        [HttpGet]
        public async Task<List<VendorDto>> GetAllAsync()
        {
            return await Mediator.Send(new GetvendorsRequest());

        }

        [HttpPost("search")]
        public Task<PaginationResponse<VendorDto>> SearchAsync(SearchVendorsRequest request)
        {
            return Mediator.Send(request);
        }

        [HttpGet("{id:int}")]
        public Task<VendorDetailsDto> GetAsync(int id)
        {
            return Mediator.Send(new GetVendorRequest(id));
        }

        [HttpPost]
        public Task<int> CreateAsync(CreateVendorRequest request)
        {
            return Mediator.Send(request);
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult<int>> UpdateAsync(UpdateVendorRequest request, int id)
        {
            return id != request.Id
                ? BadRequest()
                : Ok(await Mediator.Send(request));
        }

        [HttpPut]
        public async Task<ActionResult<int>> UpdateRangeAsync(UpdateVendorsRequest request)
        {
            return Ok(await Mediator.Send(request));
        }

        [HttpDelete("{id:int}")]
        public Task<int> DeleteAsync(int id)
        {
            return Mediator.Send(new DeleteVendorRequest(id));
        }

        [HttpPost("export")]
        public async Task<FileResult> ExportAsync(ExportVendorsRequest filter)
        {
            var result = await Mediator.Send(filter);
            return File(result, "application/octet-stream", "VendorExports");
        }

        [HttpPost("import")]
        public async Task<ActionResult<int>> ImportAsync(ImportVendorsRequest request)
        {
            return Ok(await Mediator.Send(request));
        }

    }
}