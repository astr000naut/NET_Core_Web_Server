using Dapper;
using Microsoft.AspNetCore.Mvc;
using MISA.WebFresher032023.Demo.Entities;
using MISA.WebFresher032023.Demo.RequestModel.EmployeeRequest;
using MISA.WebFresher032023.Demo.ResponseModel;
using MISA.WebFresher032023.Demo.ResponseModel.EmployeeResponse;
using MySqlConnector;
using System.Data;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MISA.WebFresher032023.Demo.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private readonly string _connectionString;
        /// <summary>
        /// Hàm khởi tạo
        /// </summary>
        /// <param name="configuration"></param>
        public EmployeesController(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("SqlConnection");
        }


        /// <summary>
        /// API filter nhân viên
        /// </summary>
        /// <param name="pageSize">Số bản ghi trên 1 trang</param>
        /// <param name="pageNumber">Vị trí trang</param>
        /// <param name="employeeFilter">Filter tìm kiếm nhân viên theo tên hoặc mã nhân viên</param>
        /// <returns></returns>
        // GET: api/v1/Employees/filter
        [Route("filter")]
        [HttpGet]
        public async Task<FilterEmployeeResponse> Get(int pageSize, int pageNumber, string? employeeFilter)
        {
            // Tạo response Object
            var response = new FilterEmployeeResponse();

            try 
            {
                // Khởi tạo kết nối đến DB
                var mySqlConnection = new MySqlConnection(_connectionString);

                // Khởi tạo paramenter cho Stored Procedure
                var dynamicParams = new DynamicParameters();
                dynamicParams.Add("p_pageSize", pageSize);
                dynamicParams.Add("p_pageNumber", pageNumber);
                dynamicParams.Add("p_employeeSearch", employeeFilter);
                dynamicParams.Add("p_totalRecord", direction: ParameterDirection.Output);

                // Thực hiện query
                var employeeList = await mySqlConnection.QueryAsync<Employee>("Proc_FilterEmployee", commandType: CommandType.StoredProcedure, param: dynamicParams);



                // Tổng số bản ghi trả về
                var employeeCount = employeeList.AsList().Count;
                // Tổng số bản ghi tìm thấy theo filter
                response.TotalRecord = dynamicParams.Get<int>("p_totalRecord");
                // Tổn số trang dữ liệu
                response.TotalPage = response.TotalRecord / pageSize;
                if (response.TotalRecord % pageSize != 0)
                    ++response.TotalPage;
                // Tính số bản ghi của trang hiện tại
                if (pageNumber < response.TotalPage)
                    response.CurrentPageRecords = pageSize;
                if (pageNumber == response.TotalPage)
                    response.CurrentPageRecords = employeeCount;
                // Vị trí trang hiện tại
                response.CurrentPage = pageNumber;
                // Danh sách nhân viên trả về
                response.Data = employeeList;
            }
            catch (Exception ex) 
            {
                response.Success = false;
                response.Message = ex.Message;
            }
            
            return response;
        }

        /// <summary>
        /// API lấy toàn bộ danh sách nhân viên
        /// </summary>
        /// <returns></returns>
        // GET: api/v1/Employees
        [HttpGet]
        public async Task<GetEmployeeListResponse> Get()
        {
            var response = new GetEmployeeListResponse();
            try {
                var mySqlConnection = new MySqlConnection(_connectionString);
                response.Data = await mySqlConnection.QueryAsync<Employee>("Proc_GetListEmployee", commandType: CommandType.StoredProcedure);
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
            }
            return response;
        }

        /// <summary>
        /// API Lấy mã nhân viên mới
        /// </summary>
        /// <returns></returns>
        [Route("NewEmployeeCode")]
        [HttpGet]
        public async Task<GetNewEmployeeCodeResponse> GetNewEmployeeCodeAsync()
        {
            var response = new GetNewEmployeeCodeResponse();
            try
            {   
                var mySqlConnection = new MySqlConnection(_connectionString);
                var dynamicParams = new DynamicParameters();

                dynamicParams.Add("o_newEmployeeCode", direction: ParameterDirection.Output);
                await mySqlConnection.QueryAsync<dynamic?>("Proc_GenerateNewEmployeeCode", commandType: CommandType.StoredProcedure, param: dynamicParams);
                response.EmployeeCode = dynamicParams.Get<string>("o_newEmployeeCode");
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
            }
            return response;
        }


        /// <summary>
        /// API tìm kiếm nhân viên theo ID
        /// </summary>
        /// <param name="id">ID nhân viên</param>
        /// <returns></returns>
        // GET: api/v1/Employees/11a8748f-3464-740c-28c2-579daad24557
        [HttpGet("{id}")]
        public async Task<GetEmployeeByIdResponse> Get(Guid id)
        {
            var response = new GetEmployeeByIdResponse();
            try 
            {
                var mySqlConnection = new MySqlConnection(_connectionString);
                var dynamicParams = new DynamicParameters();
                dynamicParams.Add("p_employeeId", id);
                var employee = await mySqlConnection.QueryFirstOrDefaultAsync<Employee?>("Proc_GetEmployeeById", commandType: CommandType.StoredProcedure, param: dynamicParams);
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
        /// API Thêm mới nhân viên
        /// </summary>
        /// <param name="employee">Object chứa thông tin nhân viên cần tạo mới</param>
        /// <returns></returns>
        // POST api/v1/Employees
        [HttpPost]
        public async Task<InsertEmployeeResponse> Post([FromBody] EmployeeRequest employee)
        {
            var response = new InsertEmployeeResponse();

            try {
                employee.ValidateDataType();

                var mySqlConnection = new MySqlConnection(_connectionString);

                // Kiểm tra trùng mã nhân viên
                var dParams = new DynamicParameters();
                dParams.Add("p_employeeCode", employee.employeeCode);
                dParams.Add("p_employeeId", null);
                dParams.Add("o_isExist", direction: ParameterDirection.Output);
                await mySqlConnection.QueryAsync<dynamic>("Proc_CheckEmployeeCodeExist", commandType: CommandType.StoredProcedure, param: dParams);

                var isEmployeeCodeExist = dParams.Get<Boolean>("o_isExist"); 
                if (isEmployeeCodeExist)
                {
                    response.ErrorCode = "101";
                    throw new Exception("Mã nhân viên đã tồn tại");
                }

                var dynamicParams = new DynamicParameters();

                var newGuidId = Guid.NewGuid();

                dynamicParams.Add("p_employeeId", newGuidId);
                foreach (var property in typeof(EmployeeRequest).GetProperties())
                {
                    dynamicParams.Add("p_" + property.Name, property.GetValue(employee));
                }
                
                await mySqlConnection.QueryAsync<Employee?>("Proc_InsertEmployee", commandType: CommandType.StoredProcedure, param: dynamicParams);
                response.EmployeeId = newGuidId;

            } catch (Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
            }
            return response;
        }

        /// <summary>
        /// Cập nhật thông tin một nhân viên
        /// </summary>
        /// <param name="id"></param>
        /// <param name="value"></param>
        // PUT api/v1/Employees/11a8748f-3464-740c-28c2-579daad24557
        [HttpPut("{id}")]
        public async Task<BaseResponse> Put(Guid id, [FromBody] EmployeeRequest employee)
        {
            var response = new BaseResponse();
            try
            {
                var mySqlConnection = new MySqlConnection(_connectionString);

                // Kiểm tra trùng mã nhân viên
                var dParams = new DynamicParameters();
                dParams.Add("p_employeeCode", employee.employeeCode);
                dParams.Add("p_employeeId", id);
                dParams.Add("o_isExist", direction: ParameterDirection.Output);
                await mySqlConnection.QueryAsync<dynamic>("Proc_CheckEmployeeCodeExist", commandType: CommandType.StoredProcedure, param: dParams);

                var isEmployeeCodeExist = dParams.Get<Boolean>("o_isExist");
                if (isEmployeeCodeExist)
                {
                    response.ErrorCode = "101";
                    throw new Exception("Mã nhân viên đã tồn tại");
                }

                var dynamicParams = new DynamicParameters();

                dynamicParams.Add("p_employeeId", id);
                foreach (var property in typeof(EmployeeRequest).GetProperties())
                {
                    dynamicParams.Add("p_" + property.Name, property.GetValue(employee));
                }

                await mySqlConnection.QueryAsync<Employee?>("Proc_UpdateEmployee", commandType: CommandType.StoredProcedure, param: dynamicParams);
            } catch (Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
            }
            return response;
        }

        /// <summary>
        /// API Xóa nhân viên
        /// </summary>
        /// <param name="id"></param>
        // DELETE api/v1/Employees/11a8748f-3464-740c-28c2-579daad24557
        [HttpDelete("{id}")]
        public async Task<DeleteEmployeeResponse> Delete(Guid id)
        {
            var response = new DeleteEmployeeResponse();

            try
            {
                var mySqlConnection = new MySqlConnection(_connectionString);
                var dynamicParams = new DynamicParameters();

                dynamicParams.Add("p_employeeId", id);
                await mySqlConnection.QueryAsync<Employee?>("Proc_DeleteEmployeeById", commandType: CommandType.StoredProcedure, param: dynamicParams);
                response.EmployeeId = id;

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
