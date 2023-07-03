using Microsoft.AspNetCore.Mvc;
using MISA.WebFresher032023.Demo.API.Controllers;
using MISA.WebFresher032023.Demo.BusinessLayer.Dtos.Input;
using MISA.WebFresher032023.Demo.BusinessLayer.Dtos.Output;
using MISA.WebFresher032023.Demo.BusinessLayer.Services;
using MISA.WebFresher032023.Demo.Common.Enums;
using MISA.WebFresher032023.Demo.Common.Exceptions;
using MISA.WebFresher032023.Demo.Common.Resources;


// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MISA.WebFresher032023.Demo.Controllers
{
    [Route("api/v1/[controller]")]
    public class EmployeesController : BaseController<EmployeeDto, EmployeeInputDto>
    {
        private readonly IEmployeeService _employeeService;

        
        public EmployeesController(IEmployeeService employeeService) : base(employeeService)
        {
            _employeeService = employeeService;
        }


        /// <summary>
        /// API Lấy mã nhân viên mới
        /// </summary>
        /// <returns>Trả về một mã nhân viên mới</returns>
        /// Author: DNT(24/05/2023)
        [Route("NewEmployeeCode")]
        [HttpGet]
        public async Task<IActionResult> GetNewEmployeeCodeAsync()
        {
            var newCode = await _employeeService.GetNewCodeAsync();
            return Ok(newCode);
        }

        /// <summary>
        /// API xuất file excel thông tin nhân viên
        /// </summary>
        /// <returns>Trả về một file excel chứa thông tin nhân viên</returns>
        /// Author: DNT(10/06/2023)
        [Route("ExportEmployeesData")]
        [HttpGet]
        public async Task<IActionResult> ExportEmployeeData()
        {
            byte[] excelFileBytes = await _employeeService.ExportEmployeesToExcelAsync();
            return File(excelFileBytes, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", EmployeeExport.FileName);
        }

    }
}
