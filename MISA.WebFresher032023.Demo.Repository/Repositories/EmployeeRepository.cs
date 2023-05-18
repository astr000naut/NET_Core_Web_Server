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
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly string _connectionString;
        /// <summary>
        /// Hàm khởi tạo - DONE
        /// </summary>
        /// <param name="configuration">Inject configuration</param>
        public EmployeeRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("SqlConnection") ?? "";
        }

        /// <summary>
        /// Khởi tạo và trả về một kết nối tới DB - DONE
        /// </summary>
        /// <returns></returns>
        public async Task<DbConnection> GetOpenConnectionAsync()
        {
            var connection = new MySqlConnection(_connectionString);
            await connection.OpenAsync();
            return connection;
        }

        /// <summary>
        /// Kiểm tra mã nhân viên đã tồn tại - DONE
        /// </summary>
        /// <param name="id"></param>
        /// <param name="employeeCode"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<Boolean> CheckCodeExistAsync(Guid? id, string employeeCode)
        {
            var connection = await GetOpenConnectionAsync();
            var dynamicParams = new DynamicParameters();
            dynamicParams.Add("p_employeeId", id);
            dynamicParams.Add("p_employeeCode", employeeCode);
            dynamicParams.Add("o_isExist", direction: ParameterDirection.Output);

            await connection.ExecuteAsync("Proc_CheckEmployeeCodeExist", commandType: CommandType.StoredProcedure, param: dynamicParams);
            await connection.CloseAsync();

            
            var isExist = dynamicParams.Get<Boolean>("o_isExist");

            return isExist;
        }

        /// <summary>
        /// Xóa nhân viên theo Id - DONE
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task DeleteByIdAsync(Guid id)
        {
            var connection = await GetOpenConnectionAsync();
            var dynamicParams = new DynamicParameters();
            dynamicParams.Add("p_employeeId", id);

            await connection.ExecuteAsync("Proc_DeleteEmployeeById", commandType: CommandType.StoredProcedure, param: dynamicParams);
            await connection.CloseAsync();
        }

        /// <summary>
        /// Filter danh sách nhân viên - DONE
        /// </summary>
        /// <param name="skip">Số lượn bỏ qua</param>
        /// <param name="take">Số lượng lấy</param>
        /// <param name="employeeSearch">Search string</param>
        /// <returns></returns>
        public async Task<EmployeeFilteredList> FilterAsync(int skip, int take, string? employeeSearch)
        {
            var connection = await GetOpenConnectionAsync();
            var dynamicParams = new DynamicParameters();
            dynamicParams.Add("p_skip", skip);
            dynamicParams.Add("p_take", take);
            dynamicParams.Add("p_employeeSearch", employeeSearch);
            dynamicParams.Add("o_totalRecord", direction: ParameterDirection.Output);

            var employeeList = await connection.QueryAsync<Employee?>("Proc_FilterEmployee", commandType: CommandType.StoredProcedure, param: dynamicParams);
            await connection.CloseAsync();

            var employeeFilterList = new EmployeeFilteredList
            {
                TotalRecord = dynamicParams.Get<int>("o_totalRecord"),
                FilteredList = employeeList
            };

            return employeeFilterList;
        }

        /// <summary>
        /// Lấy mã nhân viên mới - DONE
        /// </summary>
        /// <returns></returns>
        public async Task<string> GetNewCodeAsync()
        {
            var connection = await GetOpenConnectionAsync();
            var dynamicParams = new DynamicParameters();
            dynamicParams.Add("o_newEmployeeCode", direction: ParameterDirection.Output);

            await connection.ExecuteAsync("Proc_GenerateNewEmployeeCode", commandType: CommandType.StoredProcedure, param: dynamicParams);
            await connection.CloseAsync();

            var newEmployeeCode = dynamicParams.Get<string>("o_newEmployeeCode");

            return newEmployeeCode;
        }



        /// <summary>
        /// Lấy thông tin nhân viên theo Id - DONE
        /// </summary>
        /// <param name="employeeId"></param>
        /// <returns></returns>
        public async Task<Employee?> GetAsync(Guid employeeId)
        {
            var connection = await GetOpenConnectionAsync();
            var dynamicParams = new DynamicParameters();
            dynamicParams.Add("p_employeeId", employeeId);
            var employee = await connection.QueryFirstOrDefaultAsync<Employee?>("Proc_GetEmployeeById", commandType: CommandType.StoredProcedure, param: dynamicParams);
            await connection.CloseAsync();
            return employee;
        }

        /// <summary>
        /// Insert một nhân viên - DONE
        /// </summary>
        /// <param name="employee"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task CreateAsync(EmployeeCreate employeeCreate)
        {
            var connection = await GetOpenConnectionAsync();
            var dynamicParams = new DynamicParameters();
            dynamicParams.Add("p_employeeId", employeeCreate.EmployeeId);
            dynamicParams.Add("p_employeeCode", employeeCreate.EmployeeCode);
            dynamicParams.Add("p_employeeFullName", employeeCreate.EmployeeFullName);
            dynamicParams.Add("p_departmentId", employeeCreate.DepartmentId);
            dynamicParams.Add("p_positionName", employeeCreate.PositionName);
            dynamicParams.Add("p_dateOfBirth", employeeCreate.DateOfBirth);
            dynamicParams.Add("p_gender", employeeCreate.Gender);
            dynamicParams.Add("p_genderName", employeeCreate.GenderName);
            dynamicParams.Add("p_identityNumber", employeeCreate.IdentityNumber);
            dynamicParams.Add("p_identityDate", employeeCreate.IdentityDate);
            dynamicParams.Add("p_identityPlace", employeeCreate.IdentityPlace);
            dynamicParams.Add("p_address", employeeCreate.Address);
            dynamicParams.Add("p_phoneNumber", employeeCreate.PhoneNumber);
            dynamicParams.Add("p_landlineNumber", employeeCreate.LandlineNumber);
            dynamicParams.Add("p_email", employeeCreate.Email);
            dynamicParams.Add("p_bankAccount", employeeCreate.BankAccount);
            dynamicParams.Add("p_bankName", employeeCreate.BankName);
            dynamicParams.Add("p_bankBranch", employeeCreate.BankBranch);
            dynamicParams.Add("p_modifiedDate", employeeCreate.ModifiedDate);
            dynamicParams.Add("p_modifiedBy", employeeCreate.ModifiedBy);
            dynamicParams.Add("p_createdDate", employeeCreate.CreatedDate);
            dynamicParams.Add("p_createdBy", employeeCreate.CreatedBy);
            dynamicParams.Add("p_modifiedDate", employeeCreate.ModifiedDate);
            dynamicParams.Add("p_modifiedBy", employeeCreate.ModifiedBy);

            await connection.ExecuteAsync("Proc_InsertEmployee", commandType: CommandType.StoredProcedure, param: dynamicParams);
            await connection.CloseAsync();
        }



        /// <summary>
        /// Cập nhật thông tin nhân viên - DONE
        /// </summary>
        /// <param name="employeeId"></param>
        /// <param name="employee"></param>
        /// <returns></returns>
        public async Task UpdateAsync(Guid employeeId, EmployeeUpdate employee)
        {
            var connection = await GetOpenConnectionAsync();
            var dynamicParams = new DynamicParameters();
            dynamicParams.Add("p_employeeId", employeeId);
            dynamicParams.Add("p_employeeCode", employee.EmployeeCode);
            dynamicParams.Add("p_employeeFullName", employee.EmployeeFullName);
            dynamicParams.Add("p_departmentId", employee.DepartmentId);
            dynamicParams.Add("p_positionName", employee.PositionName);
            dynamicParams.Add("p_dateOfBirth", employee.DateOfBirth);
            dynamicParams.Add("p_gender", employee.Gender);
            dynamicParams.Add("p_genderName", employee.GenderName);
            dynamicParams.Add("p_identityNumber", employee.IdentityNumber);
            dynamicParams.Add("p_identityDate", employee.IdentityDate);
            dynamicParams.Add("p_identityPlace", employee.IdentityPlace);
            dynamicParams.Add("p_address", employee.Address);
            dynamicParams.Add("p_phoneNumber", employee.PhoneNumber);
            dynamicParams.Add("p_landlineNumber", employee.LandlineNumber);
            dynamicParams.Add("p_email", employee.Email);
            dynamicParams.Add("p_bankAccount", employee.BankAccount);
            dynamicParams.Add("p_bankName", employee.BankName);
            dynamicParams.Add("p_bankBranch", employee.BankBranch);
            dynamicParams.Add("p_modifiedDate", employee.ModifiedDate);
            dynamicParams.Add("p_modifiedBy", employee.ModifiedBy);

            await connection.ExecuteAsync("Proc_UpdateEmployee", commandType: CommandType.StoredProcedure, param: dynamicParams);
            await connection.CloseAsync();
        }
    }
}
