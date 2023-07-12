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
    public class EmployeeRepository : BaseRepository<Employee, EmployeeInput>, IEmployeeRepository
    {

        public EmployeeRepository(IDbTransaction transaction) : base(transaction) { }

        /// <summary>
        /// Lấy mã nhân viên mới
        /// </summary>
        /// <returns>Mã nhân viên mới</returns>
        /// Author: DNT(20/05/2023)
        public async Task<string> GetNewCodeAsync()
        {

            try
            {
                var dynamicParams = new DynamicParameters();
                dynamicParams.Add("o_newEmployeeCode", direction: ParameterDirection.Output);

                await _connection.ExecuteAsync("Proc_GenerateNewEmployeeCode", commandType: CommandType.StoredProcedure, param: dynamicParams, transaction: _transaction);
                var newEmployeeCode = dynamicParams.Get<string>("o_newEmployeeCode");

                return newEmployeeCode;
            }
            catch (Exception ex)
            {
                throw new DbException(Error.DbQueryFail, ex.Message, Error.DbQueryFailMsg);

            }

        }

        /// <summary>
        /// Kiểm tra ID của đơn vị khi tạo nhân viên mới
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Giá trị boolean biểu thị ID đơn vị đưa vào là hợp lệ</returns>
        /// <exception cref="DbException"></exception>
        /// Author: DNT(20/05/2023)
        public async Task<bool> ValidateDepartmentId(Guid? id)
        {
            try
            {
                var dynamicParams = new DynamicParameters();
                dynamicParams.Add("p_id", id);

                var department = await _connection.QueryFirstOrDefaultAsync<Department>("Proc_GetDepartmentById", commandType: CommandType.StoredProcedure, param: dynamicParams, transaction: _transaction);

                return department != null;
            }
            catch (Exception ex)
            {

                throw new DbException(Error.DbQueryFail, ex.Message, Error.DbQueryFailMsg);
            }
        }
    }
}
