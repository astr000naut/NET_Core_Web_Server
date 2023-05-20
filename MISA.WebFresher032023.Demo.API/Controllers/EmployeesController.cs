using Microsoft.AspNetCore.Mvc;
using MISA.WebFresher032023.Demo.BusinessLayer.Dtos.Input;
using MISA.WebFresher032023.Demo.BusinessLayer.Services;
using MISA.WebFresher032023.Demo.ResponseModel;
using MISA.WebFresher032023.Demo.ResponseModel.EmployeeResponse;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MISA.WebFresher032023.Demo.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private readonly IEmployeeService _employeeService;

        
        public EmployeesController(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }


        /// <summary> 
        /// API filter nhân viên - DONE
        /// </summary>
        /// <param name="skip">Bỏ qua bao nhiêu bản ghi</param>
        /// <param name="take">Lấy bao nhiêu bản ghi</param>
        /// <param name="employeeFilter">String pattern tìm kiếm nhân viên</param>
        /// <returns></returns>
        // GET: api/v1/Employees/filter
        [Route("filter")]
        [HttpGet]
        public async Task<FilterEmployeeResponse> FilterAsync(int skip, int take, string? keySearch)
        {

            var response = new FilterEmployeeResponse();

            try 
            {
                response.Data = await _employeeService.FilterAsync(skip, take, keySearch ?? ""); 
            }
            catch (Exception ex) 
            {
                response.Success = false;
                response.Message = ex.Message;
            }
            
            return response;
        }   

        /// <summary>
        /// API Lấy mã nhân viên mới - DONE
        /// </summary>
        /// <returns></returns>
        [Route("NewEmployeeCode")]
        [HttpGet]
        public async Task<GetNewEmployeeCodeResponse> GetNewEmployeeCodeAsync()
        {
            var response = new GetNewEmployeeCodeResponse();
            try
            {
                response.EmployeeCode = await _employeeService.GetNewCodeAsync();
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
            }
            return response;
        }


        /// <summary>
        /// API tìm kiếm nhân viên theo ID - DONE
        /// </summary>
        /// <param name="id">ID nhân viên</param>
        /// <returns></returns>
        // GET: api/v1/Employees/11a8748f-3464-740c-28c2-579daad24557
        [HttpGet("{id}")]
        public async Task<GetEmployeeByIdResponse> GetByIdAsync(Guid id)
        {
            var response = new GetEmployeeByIdResponse();
            try 
            {
                var employee = await _employeeService.GetAsync(id);
                response.Employee = employee;
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
            }
            return response;
        }

        /// <summary>
        /// API Thêm mới nhân viên - DONE
        /// </summary>
        /// <param name="employee">Object chứa thông tin nhân viên cần tạo mới</param>
        /// <returns></returns>
        // POST api/v1/Employees
        [HttpPost]
        public async Task<InsertEmployeeResponse> PostAsync([FromBody] EmployeeCreateDto employeeCreateDto)
        {
            var response = new InsertEmployeeResponse();
            try {
                var newEmployeeId = await _employeeService.CreateAsync(employeeCreateDto);
                response.EmployeeId = newEmployeeId;

            } catch (Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
            }
            return response;
        }

        /// <summary>
        /// Cập nhật thông tin một nhân viên - DONE
        /// </summary>
        /// <param name="id"></param>
        /// <param name="value"></param>
        // PUT api/v1/Employees/11a8748f-3464-740c-28c2-579daad24557
        [HttpPut("{id}")]
        public async Task<BaseResponse> PutAsync(Guid id, [FromBody] EmployeeUpdateDto employeeUpdateDto)
        {
            var response = new BaseResponse();
            try
            {
                await _employeeService.UpdateAsync(id, employeeUpdateDto);
            } catch (Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
            }
            return response;
        }

        /// <summary>
        /// API Xóa nhân viên - DONE
        /// </summary>
        /// <param name="id"></param>
        // DELETE api/v1/Employees/11a8748f-3464-740c-28c2-579daad24557
        [HttpDelete("{id}")]
        public async Task<BaseResponse> DeleteAsync(Guid id)
        {
            var response = new BaseResponse();

            try
            {
                await _employeeService.DeleteByIdAsync(id);
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
            }
            return response;
        }
    }
}
