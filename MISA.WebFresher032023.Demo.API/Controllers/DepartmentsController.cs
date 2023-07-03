using Microsoft.AspNetCore.Mvc;
using MISA.WebFresher032023.Demo.API.Controllers;
using MISA.WebFresher032023.Demo.BusinessLayer.Dtos.Input;
using MISA.WebFresher032023.Demo.BusinessLayer.Dtos.Output;
using MISA.WebFresher032023.Demo.BusinessLayer.Services;


// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MISA.WebFresher032023.Demo.Controllers
{
    [Route("api/v1/[controller]")]
    public class DepartmentsController : BaseController<DepartmentDto, DepartmentInputDto>
    {
        public DepartmentsController(IDepartmentService departmentService) : base(departmentService) { }
    }
}
