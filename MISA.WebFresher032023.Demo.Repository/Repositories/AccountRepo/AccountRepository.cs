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
using static Dapper.SqlMapper;

namespace MISA.WebFresher032023.Demo.DataLayer.Repositories.AccountRepo
{
    public class AccountRepository : BaseRepository<Account, AccountInput>, IAccountRepository
    {
        public AccountRepository(IConfiguration configuration) : base(configuration) { }

        public override async Task<bool> UpdateAsync(AccountInput accountInput)
        {
            var connection = await GetOpenConnectionAsync();

            try
            {
                var dynamicParams = new DynamicParameters();

                foreach (var property in typeof(AccountInput).GetProperties())
                {
                    var propertyNameToCamelCase = char.ToLower(property.Name[0]) + property.Name[1..];
                    var paramName = "p_" + propertyNameToCamelCase;
                    var paramValue = property.GetValue(accountInput);
                    dynamicParams.Add(paramName, paramValue);
                }

                int rowAffected = await connection.ExecuteAsync(StoredProcedureName.UpdateAccount, commandType: CommandType.StoredProcedure, param: dynamicParams);
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

        public async Task<FilteredList<Account>> FilterAccountAsync(AccountFilterInput accountFilterInput)
        {
            var connection = await GetOpenConnectionAsync();
            try
            {
                var dynamicParams = new DynamicParameters();


                var proceduredName = StoredProcedureName.FilterAccount;

                dynamicParams.Add("p_skip", accountFilterInput.Skip);
                dynamicParams.Add("p_take", accountFilterInput.Take);
                dynamicParams.Add("p_keySearch", accountFilterInput.KeySearch);
                dynamicParams.Add("p_parentIdList", accountFilterInput.ParentIdList);
                dynamicParams.Add("p_grade", accountFilterInput.Grade);
                dynamicParams.Add("o_totalRecord", direction: ParameterDirection.Output);

                var listData = await connection.QueryAsync<Account>(proceduredName,
                    commandType: CommandType.StoredProcedure, param: dynamicParams);
                var totalRecord = dynamicParams.Get<int>("o_totalRecord");

                FilteredList<Account> filteredList = new()
                {
                    ListData = listData,
                    TotalRecord = totalRecord
                };
                return filteredList;
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
