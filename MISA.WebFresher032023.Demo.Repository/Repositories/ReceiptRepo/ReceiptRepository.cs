using Dapper;
using MISA.WebFresher032023.Demo.Common.Enum;
using MISA.WebFresher032023.Demo.Common.Enums;
using MISA.WebFresher032023.Demo.Common.Exceptions;
using MISA.WebFresher032023.Demo.DataLayer.Entities.Input;
using MISA.WebFresher032023.Demo.DataLayer.Entities.Output;
using MISA.WebFresher032023.Demo.DataLayer.Repositories.GroupRepo;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Dapper.SqlMapper;

namespace MISA.WebFresher032023.Demo.DataLayer.Repositories.ReceiptRepo
{
    public class ReceiptRepository : BaseRepository<Receipt, ReceiptInput>, IReceiptRepository
    {
        public ReceiptRepository(IUnitOfWork unitOfWork): base(unitOfWork)
        {}

        public async Task<bool> DeleteReceiptDetailAsync(Guid? id)
        {
            try
            {
                var dynamicParams = new DynamicParameters();

                dynamicParams.Add("p_id", id);
                var rowAffected = await _unitOfWork.Connection.ExecuteAsync("Proc_DeleteReceiptDetailById",
                    commandType: CommandType.StoredProcedure, param: dynamicParams, transaction: _unitOfWork.Transaction);
                return rowAffected != 0;
            }
            catch (Exception ex)
            {
                throw new DbException(Error.DbQueryFail, ex.Message, Error.DbQueryFailMsg);
            }
        }

        public async Task<string> GetNewReceiptNoAsync()
        {
            try
            {
                var dynamicParams = new DynamicParameters();
                dynamicParams.Add("o_newReceiptNo", direction: ParameterDirection.Output);

                await _unitOfWork.Connection.ExecuteAsync("Proc_GenerateNewReceiptNo", commandType: CommandType.StoredProcedure, param: dynamicParams, transaction: _unitOfWork.Transaction);
                var newReceiptNo = dynamicParams.Get<string>("o_newReceiptNo");

                return newReceiptNo;
            }
            catch (Exception ex)
            {
                throw new DbException(Error.DbQueryFail, ex.Message, Error.DbQueryFailMsg);

            }
        }

        public async Task<IEnumerable<ReceiptDetail>> GetReceiptDetailListAsync(Guid id)
        {
            try
            {
                var dynamicParams = new DynamicParameters();

                dynamicParams.Add("p_id", id);
                var listReceiptDetail = await _unitOfWork.Connection.QueryAsync<ReceiptDetail>("Proc_GetReceiptDetailList",
                    commandType: CommandType.StoredProcedure, param: dynamicParams, transaction: _unitOfWork.Transaction);
                return listReceiptDetail;
            }
            catch (Exception ex)
            {
                throw new DbException(Error.DbQueryFail, ex.Message, Error.DbQueryFailMsg);
            }
        }

        public async Task<long> GetTotalReceive(string keySearch)
        {
            try
            {
                var dynamicParams = new DynamicParameters();
                dynamicParams.Add("p_keySearch", keySearch);
                dynamicParams.Add("o_totalReceive", direction: ParameterDirection.Output);
                await _unitOfWork.Connection.ExecuteAsync("Proc_GetReceiptTotalReceive",
                    commandType: CommandType.StoredProcedure, param: dynamicParams, transaction: _unitOfWork.Transaction);
                var totalReceive = dynamicParams.Get<decimal?>("o_totalReceive");
                if (totalReceive != null)
                    return (long) totalReceive;
                return 0;
            }
            catch (Exception ex)
            {
                throw new DbException(Error.DbQueryFail, ex.Message, Error.DbQueryFailMsg);
            }
        }

        public async Task<bool> InsertReceiptDetailAsync(ReceiptDetailInput receiptDetailInput)
        {
            try
            {
                var dynamicParams = new DynamicParameters();

                foreach (var property in typeof(ReceiptDetailInput).GetProperties())
                {
                    var propertyNameToCamelCase = char.ToLower(property.Name[0]) + property.Name[1..];
                    var paramName = "p_" + propertyNameToCamelCase;
                    var paramValue = property.GetValue(receiptDetailInput);
                    dynamicParams.Add(paramName, paramValue);
                }
                var rowAffected = await _unitOfWork.Connection.ExecuteAsync("Proc_InsertReceiptDetail", commandType: CommandType.StoredProcedure, param: dynamicParams, transaction: _unitOfWork.Transaction);

                return (rowAffected != 0);

            }
            catch (Exception ex)
            {
                throw new DbException(Error.DbQueryFail, ex.Message, Error.DbQueryFailMsg);
            }
        }

        public async Task<bool> UpdateReceiptDetailAsync(ReceiptDetailInput receiptDetailInput)
        {
            try
            {
                var queryString = $"UPDATE receiptdetail SET ";

                foreach (var property in typeof(ReceiptDetailInput).GetProperties())
                {
                    var paramValue = property.GetValue(receiptDetailInput);
                    if (paramValue != null && property.Name != "ReceiptDetaiId")
                    {
                        queryString += $"{property.Name} = @{property.Name} ,";
                    }

                }

                queryString = queryString.Remove(queryString.Length - 1);
                queryString += $"WHERE ReceiptDetailId = @ReceiptDetailId;";

                var rowAffected = await _unitOfWork.Connection.ExecuteAsync(queryString, receiptDetailInput, transaction: _unitOfWork.Transaction);
                return (rowAffected != 0);

            }
            catch (Exception ex)
            {
                throw new DbException(Error.DbQueryFail, ex.Message, Error.DbQueryFailMsg);
            }

        }
    }
}
