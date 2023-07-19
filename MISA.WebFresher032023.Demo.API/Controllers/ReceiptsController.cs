using Microsoft.AspNetCore.Mvc;
using MISA.WebFresher032023.Demo.BusinessLayer.Dtos.Input;
using MISA.WebFresher032023.Demo.BusinessLayer.Dtos.Output;
using MISA.WebFresher032023.Demo.BusinessLayer.Services;
using MISA.WebFresher032023.Demo.BusinessLayer.Services.GroupSvc;
using MISA.WebFresher032023.Demo.BusinessLayer.Services.ReceiptSvc;

namespace MISA.WebFresher032023.Demo.API.Controllers
{
    [Route("api/v1/[controller]")]
    public class ReceiptsController : BaseController<ReceiptDto, ReceiptInputDto>
    {

        private readonly IReceiptService _receiptService;


        public ReceiptsController(IReceiptService receiptService) : base(receiptService)
        {
            _receiptService = receiptService;
        }


        [Route("NewReceiptNo")]
        [HttpGet]
        public async Task<IActionResult> GetNewReceiptNoAsync()
        {
            var newReceiptNo = await _receiptService.GetNewReceiptNoAsync();
            return Ok(newReceiptNo);
        }

        [Route("ExportData")]
        [HttpPost]
        public async Task<IActionResult> ExportReceipData(ExportExcelDto exportExcelDto)
        {
            byte[] excelFileBytes = await _receiptService.ExportExcelAsync(exportExcelDto);
            return File(excelFileBytes, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", exportExcelDto.FileName);
        }

        [Route("GetTotalReceive")]
        [HttpGet]
        public async Task<IActionResult> GetTotalReceive(string? keySearch)
        {
            var totalReceive = await _receiptService.GetTotalReceive(keySearch);
            return Ok(totalReceive);
        }
    }
}
