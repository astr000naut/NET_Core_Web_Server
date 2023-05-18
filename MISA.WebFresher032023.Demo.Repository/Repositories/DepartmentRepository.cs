using Dapper;
using Microsoft.Extensions.Configuration;
using MISA.WebFresher032023.Demo.DataLayer.Entities.Input;
using MISA.WebFresher032023.Demo.DataLayer.Entities.Output;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WebFresher032023.Demo.DataLayer.Repositories
{
    public class DepartmentRepository : IDepartmentRepository
    {
        private readonly string _connectionString;

        /// <summary>
        /// Hàm khởi tạo - DONE
        /// </summary>
        /// <param name="configuration">Inject configuration</param>
        public DepartmentRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("SqlConnection") ?? "";
        }

        /// <summary>
        /// Khởi tạo và trả về kết nối đến DB - DONE
        /// </summary>
        /// <returns></returns>
        public async Task<DbConnection> GetOpenConnectionAsync()
        {
            var connection = new MySqlConnection(_connectionString);
            await connection.OpenAsync();
            return connection;
        }

        /// <summary>
        /// Xóa Department - DONE
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task DeleteByIdAsync(Guid id)
        {
            var connection = await GetOpenConnectionAsync();
            var dynamicParams = new DynamicParameters();
            dynamicParams.Add("p_departmentId", id);

            await connection.ExecuteAsync("Proc_DeleteDepartmentById", commandType: CommandType.StoredProcedure, param: dynamicParams);
            await connection.CloseAsync();
        }
        
        /// <summary>
        /// Lấy thông tin department theo ID - DONE
        /// </summary>
        /// <param name="departmentId"></param>
        /// <returns></returns>
        public async Task<Department?> GetAsync(Guid departmentId)
        {
            var connection = await GetOpenConnectionAsync();
            var dynamicParams = new DynamicParameters();
            dynamicParams.Add("p_departmentId", departmentId);
            var department = await connection.QueryFirstOrDefaultAsync<Department?>("Proc_GetDepartmentById", commandType: CommandType.StoredProcedure, param: dynamicParams);
            await connection.CloseAsync();
            return department;
        }

        /// <summary>
        /// Lấy danh sách department - DONE
        /// </summary>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<IEnumerable<Department?>> GetAllAsync()
        {
            var connection = await GetOpenConnectionAsync();
            var allDepartment = await connection.QueryAsync<Department?>("Proc_GetListDepartment", commandType: CommandType.StoredProcedure);
            await connection.CloseAsync();
            return allDepartment;
        }

        /// <summary>
        /// Thêm mới một Department
        /// </summary>
        /// <param name="department"></param>
        /// <returns></returns>
        public async Task CreateAsync(DepartmentCreate departmentCreate)
        {
            var connection = await GetOpenConnectionAsync();
            var dynamicParams = new DynamicParameters();
            dynamicParams.Add("p_departmentId", departmentCreate.DepartmentId);
            dynamicParams.Add("p_departmentCode", departmentCreate.DepartmentCode);
            dynamicParams.Add("p_departmentName", departmentCreate.DepartmentName);
            dynamicParams.Add("p_createdDate", departmentCreate.CreatedDate);
            dynamicParams.Add("p_createdBy", departmentCreate.CreatedBy);
            dynamicParams.Add("p_modifiedDate", departmentCreate.ModifiedDate);
            dynamicParams.Add("p_modifiedby", departmentCreate.ModifiedBy);
            await connection.ExecuteAsync("Proc_InsertDepartment", commandType: CommandType.StoredProcedure, param: dynamicParams);
            await connection.CloseAsync();
        }

        /// <summary>
        /// Cập nhật thông tin Department - DONE
        /// </summary>
        /// <param name="departmentId"></param>
        /// <param name="departmentUpdate"></param>
        /// <returns></returns>
        public async Task UpdateAsync(Guid departmentId, DepartmentUpdate departmentUpdate)
        {
            var connection = await GetOpenConnectionAsync();
            var dynamicParams = new DynamicParameters();
            dynamicParams.Add("p_departmentId", departmentId);
            dynamicParams.Add("p_departmentCode", departmentUpdate.DepartmentCode);
            dynamicParams.Add("p_departmentName", departmentUpdate.DepartmentName);
            dynamicParams.Add("p_modifiedDate", departmentUpdate.ModifiedDate);
            dynamicParams.Add("p_modifiedby", departmentUpdate.ModifiedBy);
            await connection.ExecuteAsync("Proc_InsertDepartment", commandType: CommandType.StoredProcedure, param: dynamicParams);
            await connection.CloseAsync();
        }
    }
}
