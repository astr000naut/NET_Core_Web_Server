using Dapper;
using Microsoft.Extensions.Configuration;
using MISA.WebFresher032023.Demo.Common.Enums;
using MISA.WebFresher032023.Demo.Common.Exceptions;
using MISA.WebFresher032023.Demo.DataLayer.Entities.Input;
using MISA.WebFresher032023.Demo.DataLayer.Entities.Output;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WebFresher032023.Demo.DataLayer.Repositories
{
    public class CustomerRepository : BaseRepository<Customer, CustomerCreate, CustomerUpdate>, ICustomerRepository
    {
        public CustomerRepository(IConfiguration configuration) : base(configuration) { }

        /// <summary>
        /// Lấy mã khách hàng mới
        /// </summary>
        /// <returns>Mã khách hàng mới</returns>
        /// Author: DNT(20/06/2023)
        public async Task<string> GetNewCodeAsync()
        {
            var connection = await GetOpenConnectionAsync();

            try
            {
                var dynamicParams = new DynamicParameters();
                dynamicParams.Add("o_newCustomerCode", direction: ParameterDirection.Output);

                await connection.ExecuteAsync("Proc_GenerateNewCustomerCode", commandType: CommandType.StoredProcedure, param: dynamicParams);
                var newEmployeeCode = dynamicParams.Get<string>("o_newCustomerCode");

                return newEmployeeCode;
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

        public override async Task<bool> UpdateAsync(Guid id, CustomerUpdate customerUpdate)
        {
            var connection = await GetOpenConnectionAsync();

            try
            {
                var dynamicParams = new DynamicParameters();
                // dynamicParams.Add("o_newCustomerCode", direction: ParameterDirection.Output);

                foreach (var property in typeof(CustomerUpdate).GetProperties())
                {
                    var propertyNameToCamelCase = char.ToLower(property.Name[0]) + property.Name[1..];
                    var paramName = "p_" + propertyNameToCamelCase;
                    var paramValue = property.GetValue(customerUpdate);
                    dynamicParams.Add(paramName, paramValue);
                }

                int rowAffected = await connection.ExecuteAsync("Proc_UpdateCustomer", commandType: CommandType.StoredProcedure, param: dynamicParams);
                return rowAffected > 0;
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