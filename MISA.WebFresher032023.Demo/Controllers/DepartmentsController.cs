using Dapper;
using Microsoft.AspNetCore.Mvc;
using MISA.WebFresher032023.Demo.Entities;
using MISA.WebFresher032023.Demo.RequestModel.DepartmentRequest;
using MISA.WebFresher032023.Demo.ResponseModel;
using MISA.WebFresher032023.Demo.ResponseModel.DepartmentResponse;
using MySqlConnector;
using System.Data;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MISA.WebFresher032023.Demo.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class DepartmentsController : ControllerBase
    {
        private readonly string _connectionString;
        public DepartmentsController(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("SqlConnection");
        }

        /// <summary>
        /// API lấy danh sách các Đơn vị
        /// </summary>
        /// <returns></returns>
        // GET: api/<DepartmentsController>
        [HttpGet]
        public async Task<GetDepartmentListResponse> Get()
        {
            var response = new GetDepartmentListResponse();
            try
            {
                var mySqlConnection = new MySqlConnection(_connectionString);
                response.Data = await mySqlConnection.QueryAsync<Department>("Proc_GetListDepartment", commandType: CommandType.StoredProcedure);
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
            }
            return response;

        }

        /// <summary>
        /// API lấy thông tin một đơn vị theo Id
        /// </summary>
        /// <returns></returns>
        // GET api/<DepartmentsController>/5
        [HttpGet("{id}")]
        public async Task<GetDepartmentByIdResponse> Get(Guid id)
        {
            var response = new GetDepartmentByIdResponse();
            try
            {
                var mySqlConnection = new MySqlConnection(_connectionString);
                var dynamicParams = new DynamicParameters();
                dynamicParams.Add("p_departmentId", id);
                var department = await mySqlConnection.QueryFirstOrDefaultAsync<Department?>("Proc_GetDepartmentById", commandType: CommandType.StoredProcedure, param: dynamicParams);
                response.Department = department;
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
            }
            return response;
        }

        /// <summary>
        /// API thêm mới đơn vị
        /// </summary>
        /// <param name="department"></param>
        /// <returns></returns>
        // POST api/<DepartmentsController>
        [HttpPost]
        public async Task<InsertDepartmentResponse> Post([FromBody] DepartmentRequest department)
        {
            var response = new InsertDepartmentResponse();

            try
            {
                department.ValidateDataType();

                var mySqlConnection = new MySqlConnection(_connectionString);

                // Kiểm tra trùng mã đơn vị
                var dParams = new DynamicParameters();
                dParams.Add("p_departmentCode", department.departmentCode);
                dParams.Add("p_departmentId", null);
                dParams.Add("o_isExist", direction: ParameterDirection.Output);
                await mySqlConnection.QueryAsync<dynamic>("Proc_CheckDepartmentCodeExist", commandType: CommandType.StoredProcedure, param: dParams);

                var isDepartmentCodeExist = dParams.Get<Boolean>("o_isExist");
                if (isDepartmentCodeExist)
                {
                    response.ErrorCode = "202";
                    throw new Exception("Mã đơn vị đã tồn tại");
                }

                var dynamicParams = new DynamicParameters();

                var newGuidId = Guid.NewGuid();

                dynamicParams.Add("p_departmentId", newGuidId);
                foreach (var property in typeof(DepartmentRequest).GetProperties())
                {
                    dynamicParams.Add("p_" + property.Name, property.GetValue(department));
                }

                await mySqlConnection.QueryAsync<dynamic?>("Proc_InsertDepartment", commandType: CommandType.StoredProcedure, param: dynamicParams);
                response.DepartmentId = newGuidId;

            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
            }
            return response;
        }

        /// <summary>
        /// API sửa thông tin đơn vị
        /// </summary>
        /// <param name="id"></param>
        /// <param name="value"></param>
        // PUT api/<DepartmentsController>/5
        [HttpPut("{id}")]
        public async Task<BaseResponse> Put(Guid id, [FromBody] DepartmentRequest department)
        {
            var response = new BaseResponse();
            try
            {
                var mySqlConnection = new MySqlConnection(_connectionString);

                // Kiểm tra trùng mã đơn vị
                var dParams = new DynamicParameters();
                dParams.Add("p_departmentCode", department.departmentCode);
                dParams.Add("p_departmentId", id);
                dParams.Add("o_isExist", direction: ParameterDirection.Output);
                await mySqlConnection.QueryAsync<dynamic>("Proc_CheckDepartmentCodeExist", commandType: CommandType.StoredProcedure, param: dParams);

                var isDepartmentCodeExist = dParams.Get<Boolean>("o_isExist");
                if (isDepartmentCodeExist)
                {
                    response.ErrorCode = "202";
                    throw new Exception("Mã đơn vị đã tồn tại");
                }

                var dynamicParams = new DynamicParameters();

                dynamicParams.Add("p_departmentId", id);
                foreach (var property in typeof(DepartmentRequest).GetProperties())
                {
                    dynamicParams.Add("p_" + property.Name, property.GetValue(department));
                }

                await mySqlConnection.QueryAsync<dynamic?>("Proc_UpdateDepartment", commandType: CommandType.StoredProcedure, param: dynamicParams);
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
            }
            return response;
        }

        /// <summary>
        /// API xóa đơn vị theo ID
        /// </summary>
        /// <param name="id"></param>
        // DELETE api/<DepartmentsController>/5
        [HttpDelete("{id}")]
        public async Task<BaseResponse> Delete(Guid id)
        {
            var response = new BaseResponse();

            try
            {
                var mySqlConnection = new MySqlConnection(_connectionString);
                var dynamicParams = new DynamicParameters();

                dynamicParams.Add("p_departmentId", id);
                await mySqlConnection.QueryAsync<dynamic?>("Proc_DeleteDepartmentById", commandType: CommandType.StoredProcedure, param: dynamicParams);
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
