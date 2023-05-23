﻿using Dapper;
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
        /// Kiểm tra mã nhân viên đã tồn tại
        /// </summary>
        /// <param name="id"></param>
        /// <param name="employeeCode"></param>
        /// <returns></returns>
        /// Author: DNT(20/05/2023)
        public async Task<Boolean> CheckCodeExistAsync(Guid? id, string employeeCode)
        {
            var connection = await GetOpenConnectionAsync();
            try
            {
                var dynamicParams = new DynamicParameters();
                dynamicParams.Add("p_employeeId", id);
                dynamicParams.Add("p_employeeCode", employeeCode);
                dynamicParams.Add("o_isExist", direction: ParameterDirection.Output);

                await connection.ExecuteAsync("Proc_CheckEmployeeCodeExist", commandType: CommandType.StoredProcedure, param: dynamicParams);
                var isExist = dynamicParams.Get<Boolean>("o_isExist");
                await connection.CloseAsync();
                return isExist;
            } catch (Exception ex)
            {
                await connection.CloseAsync();
                throw new DbException(Error.DbQueryFail, ex.Message, Error.DbQueryFailMsg);
            }
        }

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
                await connection.CloseAsync();

                return newEmployeeCode;
            } catch (Exception ex)
            {
                await connection.CloseAsync();
                throw new DbException(Error.DbQueryFail, ex.Message, Error.DbQueryFailMsg);
            }
        }

        /// <summary>
        /// Kiểm tra ID của đơn vị khi tạo nhân viên mới
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <exception cref="DbException"></exception>
        public async Task<bool> ValidateDepartmentId(Guid? id)
        {
            var connection = await GetOpenConnectionAsync();
            try
            {
                var dynamicParams = new DynamicParameters();
                dynamicParams.Add("p_id", id);

                var department = await connection.QueryFirstOrDefaultAsync<Department>("Proc_GetDepartmentById", commandType: CommandType.StoredProcedure, param: dynamicParams);
                await connection.CloseAsync();

                return (department != null);
            }
            catch (Exception ex)
            {
                await connection.CloseAsync();
                throw new DbException(Error.DbQueryFail, ex.Message, Error.DbQueryFailMsg);
            }
        }
    }
}
