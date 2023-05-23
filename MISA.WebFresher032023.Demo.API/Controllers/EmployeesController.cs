using Microsoft.AspNetCore.Mvc;
using MISA.WebFresher032023.Demo.API.Controllers;
using MISA.WebFresher032023.Demo.BusinessLayer.Dtos.Input;
using MISA.WebFresher032023.Demo.BusinessLayer.Dtos.Output;
using MISA.WebFresher032023.Demo.BusinessLayer.Services;
using MISA.WebFresher032023.Demo.Common.Enums;
using MISA.WebFresher032023.Demo.Common.Exceptions;


// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MISA.WebFresher032023.Demo.Controllers
{
    [Route("api/v1/[controller]")]
    public class EmployeesController : BaseController<EmployeeDto, EmployeeCreateDto, EmployeeUpdateDto>
    {
        private readonly IEmployeeService _employeeService;

        
        public EmployeesController(IEmployeeService employeeService) : base(employeeService)
        {
            _employeeService = employeeService;
        }


        /// <summary>
        /// API Lấy mã nhân viên mới - DONE
        /// </summary>
        /// <returns></returns>
        [Route("NewEmployeeCode")]
        [HttpGet]
        public async Task<string> GetNewEmployeeCodeAsync()
        {
            return await _employeeService.GetNewCodeAsync();
        }

    }
}
