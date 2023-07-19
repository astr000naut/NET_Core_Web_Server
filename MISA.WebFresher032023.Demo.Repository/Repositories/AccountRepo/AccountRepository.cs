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
        public AccountRepository(IUnitOfWork unitOfWork) : base(unitOfWork) { }

        public override async Task<bool> UpdateAsync(AccountInput accountInput)
        {
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

                int rowAffected = await _unitOfWork.Connection.ExecuteAsync(StoredProcedureName.UpdateAccount, commandType: CommandType.StoredProcedure, param: dynamicParams, transaction: _unitOfWork.Transaction);
                return rowAffected > 0;
            }
            catch (Exception ex)
            {
                throw new DbException(Error.DbQueryFail, ex.Message, Error.DbQueryFailMsg);
            }
        }

        public async Task<FilteredList<Account>> FilterAccountAsync(AccountFilterInput accountFilterInput)
        {
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

                var listData = await _unitOfWork.Connection.QueryAsync<Account>(proceduredName,
                    commandType: CommandType.StoredProcedure, param: dynamicParams, transaction: _unitOfWork.Transaction);
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
        }

        public async Task<bool> ChangeUsingStatusAsync(string mCodeId, bool usingStatus)
        {
            try
            {
                var dynamicParams = new DynamicParameters();


                var proceduredName = "Proc_ChangeAccountUsingStatus";

                var usingStatusName = UsingStatus.isUsing;
                if (!usingStatus) usingStatusName = UsingStatus.isNotUsing;

                dynamicParams.Add("p_mCodeId", mCodeId);
                dynamicParams.Add("p_usingStatus", usingStatus);
                dynamicParams.Add("p_usingStatusName", usingStatusName);


                var rowAffected = await _unitOfWork.Connection.ExecuteAsync(proceduredName,
                    commandType: CommandType.StoredProcedure, param: dynamicParams, transaction: _unitOfWork.Transaction);
                return rowAffected != 0;
            }
            catch (Exception ex)
            {
                throw new DbException(Error.DbQueryFail, ex.Message, Error.DbQueryFailMsg);
            }
        }
    }
}
