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
    public class EmployeeRepository : BaseRepository<Employee, EmployeeCreate, EmployeeUpdate>, IEmployeeRepository
    {
      
        public EmployeeRepository(IConfiguration configuration): base(configuration) { }

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

    }
}
