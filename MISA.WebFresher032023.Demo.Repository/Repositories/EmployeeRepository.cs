using Dapper;
using Microsoft.Extensions.Configuration;
using MISA.WebFresher032023.Demo.Common.Enums;
using MISA.WebFresher032023.Demo.Common.Exceptions;
using MISA.WebFresher032023.Demo.DataLayer.Entities.Input;
using MISA.WebFresher032023.Demo.DataLayer.Entities.Output;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WebFresher032023.Demo.DataLayer.Repositories
{
    public class EmployeeRepository : BaseRepository<Employee, EmployeeCreate, EmployeeUpdate>, IEmployeeRepository
    {
      
        public EmployeeRepository(IConfiguration configuration): base(configuration) { }

        /// <summary>
        /// Lấy mã nhân viên mới
        /// </summary>
        /// <returns></returns>
        /// Author: DNT(20/05/2023)
        public async Task<string> GetNewCodeAsync()
        {
            var connection = await GetOpenConnectionAsync();
           
            try
            {
                var dynamicParams = new DynamicParameters();
                dynamicParams.Add("o_newEmployeeCode", direction: ParameterDirection.Output);
              
                await connection.ExecuteAsync("Proc_GenerateNewEmployeeCode", commandType: CommandType.StoredProcedure, param: dynamicParams);
                var newEmployeeCode = dynamicParams.Get<string>("o_newEmployeeCode");

                return newEmployeeCode;
            } catch (Exception ex)
            {   
                throw new DbException(Error.DbQueryFail, ex.Message, Error.DbQueryFailMsg);

            } finally 
            {
                await connection.CloseAsync();
            }

        }

        /// <summary>
        /// Kiểm tra ID của đơn vị khi tạo nhân viên mới
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <exception cref="DbException"></exception>
        /// Author: DNT(20/05/2023)
        public async Task<bool> ValidateDepartmentId(Guid? id)
        {
            var connection = await GetOpenConnectionAsync();
            try
            {
                var dynamicParams = new DynamicParameters();
                dynamicParams.Add("p_id", id);

                var department = await connection.QueryFirstOrDefaultAsync<Department>("Proc_GetDepartmentById", commandType: CommandType.StoredProcedure, param: dynamicParams);

                return (department != null);
            }
            catch (Exception ex)
            {
                
                throw new DbException(Error.DbQueryFail, ex.Message, Error.DbQueryFailMsg);
            }
            finally
            {
                await connection.CloseAsync();
            }
        }
    }
}
