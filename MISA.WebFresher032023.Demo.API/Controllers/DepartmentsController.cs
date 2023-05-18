using Microsoft.AspNetCore.Mvc;
using MISA.WebFresher032023.Demo.BusinessLayer.DepartmentService;
using MISA.WebFresher032023.Demo.BusinessLayer.DepartmentService.Dto.FromClient;
using MISA.WebFresher032023.Demo.ResponseModel;
using MISA.WebFresher032023.Demo.ResponseModel.DepartmentResponse;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MISA.WebFresher032023.Demo.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class DepartmentsController : ControllerBase
    {
        private readonly IDepartmentService _departmentService;
        public DepartmentsController(IDepartmentService departmentService)
        {
            _departmentService = departmentService;
        }

        /// <summary>
        /// API lấy danh sách các Đơn vị - DONE
        /// </summary>
        /// <returns></returns>
        // GET: api/<DepartmentsController>
        [HttpGet]
        public async Task<GetDepartmentListResponse> GetAllAsync()
        {
            var response = new GetDepartmentListResponse();
            try
            {
                response.Data = await _departmentService.GetAllAsync();
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
            }
            return response;

        }

        /// <summary>
        /// API lấy thông tin một đơn vị theo Id - DONE
        /// </summary>
        /// <returns></returns>
        // GET api/<DepartmentsController>/5
        [HttpGet("{id}")]
        public async Task<GetDepartmentByIdResponse> GetAsync(Guid id)
        {
            var response = new GetDepartmentByIdResponse();
            try
            {
                response.Department = await _departmentService.GetAsync(id);
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
            }
            return response;
        }

        /// <summary>
        /// API thêm mới đơn vị - DONE
        /// </summary>
        /// <param name="department"></param>
        /// <returns></returns>
        // POST api/<DepartmentsController>
        [HttpPost]
        public async Task<InsertDepartmentResponse> PostAsync([FromBody] DepartmentCreateDto departmentCreateDto)
        {
            var response = new InsertDepartmentResponse();

            try
            {
                await _departmentService.CreateAsync(departmentCreateDto);
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
            }
            return response;
        }

        /// <summary>
        /// API sửa thông tin đơn vị - DONE
        /// </summary>
        /// <param name="id"></param>
        /// <param name="value"></param>
        // PUT api/<DepartmentsController>/5
        [HttpPut("{id}")]
        public async Task<BaseResponse> PutAsync(Guid id, [FromBody] DepartmentUpdateDto departmentUpdateDto)
        {
            var response = new BaseResponse();
            try
            {
               await _departmentService.UpdateAsync(id, departmentUpdateDto);
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
            }
            return response;
        }

        /// <summary>
        /// API xóa đơn vị theo ID - DONE
        /// </summary>
        /// <param name="id"></param>
        // DELETE api/<DepartmentsController>/5
        [HttpDelete("{id}")]
        public async Task<BaseResponse> DeleteAsync(Guid id)
        {
            var response = new BaseResponse();

            try
            {
                await _departmentService.DeleteByIdAsync(id);
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
