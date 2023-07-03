

using Microsoft.AspNetCore.Mvc;
using MISA.WebFresher032023.Demo.BusinessLayer.Dtos.Input;
using MISA.WebFresher032023.Demo.BusinessLayer.Dtos.Output;
using MISA.WebFresher032023.Demo.BusinessLayer.Services;

namespace MISA.WebFresher032023.Demo.API.Controllers
{
    [Route("api/v1/[controller]")]
    public class CustomersController : BaseController<CustomerDto, CustomerInputDto>
    {
        private readonly ICustomerService _customerService;

        public CustomersController(ICustomerService customerService) : base(customerService) { 
            _customerService = customerService;
        }

        [Route("NewCustomerCode")]
        [HttpGet]
        public async Task<IActionResult> GetNewEmployeeCodeAsync()
        {
            var newCode = await _customerService.GetNewCodeAsync();
            return Ok(newCode);
        }
    }
}
