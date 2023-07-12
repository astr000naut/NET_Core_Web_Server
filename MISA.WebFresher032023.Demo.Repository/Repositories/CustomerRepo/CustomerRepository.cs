using Dapper;
using Microsoft.Extensions.Configuration;
using MISA.WebFresher032023.Demo.Common.Enum;
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
    public class CustomerRepository : BaseRepository<Customer, CustomerInput>, ICustomerRepository
    {
        public CustomerRepository(IUnitOfWork unitOfWork) : base(unitOfWork) { }

        /// <summary>
        /// Lấy mã khách hàng mới
        /// </summary>
        /// <returns>Mã khách hàng mới</returns>
        /// Author: DNT(20/06/2023)
        public async Task<string> GetNewCodeAsync()
        {
            try
            {
                var dynamicParams = new DynamicParameters();
                dynamicParams.Add("o_newCustomerCode", direction: ParameterDirection.Output);

                await _unitOfWork.Connection.ExecuteAsync("Proc_GenerateNewCustomerCode", commandType: CommandType.StoredProcedure, param: dynamicParams, transaction: _unitOfWork.Transaction);
                var newEmployeeCode = dynamicParams.Get<string>("o_newCustomerCode");

                return newEmployeeCode;
            }
            catch (Exception ex)
            {
                throw new DbException(Error.DbQueryFail, ex.Message, Error.DbQueryFailMsg);

            }


        }

        public override async Task<bool> UpdateAsync(CustomerInput customerInput)
        {

            try
            {
                var dynamicParams = new DynamicParameters();
                // dynamicParams.Add("o_newCustomerCode", direction: ParameterDirection.Output);

                foreach (var property in typeof(CustomerInput).GetProperties())
                {
                    var propertyNameToCamelCase = char.ToLower(property.Name[0]) + property.Name[1..];
                    var paramName = "p_" + propertyNameToCamelCase;
                    var paramValue = property.GetValue(customerInput);
                    dynamicParams.Add(paramName, paramValue);
                }

                int rowAffected = await _unitOfWork.Connection.ExecuteAsync(StoredProcedureName.UpdateCustomer, commandType: CommandType.StoredProcedure, param: dynamicParams, transaction: _unitOfWork.Transaction);
                return rowAffected > 0;
            }
            catch (Exception ex)
            {
                throw new DbException(Error.DbQueryFail, ex.Message, Error.DbQueryFailMsg);
            }
        }

    }
}