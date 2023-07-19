using Microsoft.AspNetCore.Mvc;
using MISA.WebFresher032023.Demo.BusinessLayer.Dtos.Input;
using MISA.WebFresher032023.Demo.BusinessLayer.Dtos.Output;
using MISA.WebFresher032023.Demo.BusinessLayer.Services.AccountSvc;
using MISA.WebFresher032023.Demo.Common.Enums;
using MISA.WebFresher032023.Demo.Common.Exceptions;
using MISA.WebFresher032023.Demo.Common.Resources;

namespace MISA.WebFresher032023.Demo.API.Controllers
{
    [Route("api/v1/[controller]")]
    public class AccountsController : BaseController<AccountDto, AccountInputDto>
    {
        private readonly IAccountService _accountService;

        public AccountsController(IAccountService accountService) : base(accountService)
        {
            _accountService = accountService;
        }

        [Route("FilterAccount")]
        [HttpGet]
        public async Task<IActionResult> FilterAccountAsync([FromQuery] AccountFilterInputDto accountFilterInputDto)
        {

            var filteredList = await _accountService.FilterAccountAsync(accountFilterInputDto);
            return Ok(filteredList);
        }

        [Route("ExportData")]
        [HttpPost]
        public async Task<IActionResult> ExportAccountData(ExportExcelDto exportExcelDto)
        {
            byte[] excelFileBytes = await _accountService.ExportExcelAsync(exportExcelDto);
            return File(excelFileBytes, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", exportExcelDto.FileName);
        }

        [Route("ChangeUsingStatus")]
        [HttpPut]
        public async Task<IActionResult> ChangeUsingStatus(AccountChangeUsingStatusDto accountChangeUsingStatusDto)
        {
            var result = await _accountService.ChangeUsingStatusAsync(accountChangeUsingStatusDto);
            return Ok(result);
        }
    }
}
