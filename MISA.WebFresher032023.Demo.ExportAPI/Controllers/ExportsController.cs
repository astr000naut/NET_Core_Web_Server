using Microsoft.AspNetCore.Mvc;
using MISA.WebFresher032023.Demo.Common.Resources;
using MISA.WebFresher032023.Demo.ExportService.Dtos.Input;
using MISA.WebFresher032023.Demo.ExportService.Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MISA.WebFresher032023.Demo.ExportAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExportsController : ControllerBase
    {
        private readonly IExportService _exportService;
        public ExportsController(IExportService exportService)
        {
            _exportService = exportService;
        }

        [Route("ExportExcel")]
        [HttpPost]
        public async Task<IActionResult> ExportExelAsync(ExportExcelInputDto exportExcelInputDto)
        {
            byte[] excelFileBytes = await _exportService.ExportExcelAsync(exportExcelInputDto);
            return File(excelFileBytes, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "FileName");
        }


    }
}
