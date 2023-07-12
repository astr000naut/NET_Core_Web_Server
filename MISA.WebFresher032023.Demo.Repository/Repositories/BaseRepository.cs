using Dapper;
using Microsoft.Extensions.Configuration;
using MISA.WebFresher032023.Demo.Common.Enum;
using MISA.WebFresher032023.Demo.Common.Enums;
using MISA.WebFresher032023.Demo.Common.Exceptions;
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
using DbException = MISA.WebFresher032023.Demo.Common.Exceptions.DbException;

namespace MISA.WebFresher032023.Demo.DataLayer.Repositories
{
    public abstract class BaseRepository<TEntity, TEntityInput> 
        : IBaseRepository<TEntity, TEntityInput>

    {
        protected readonly IUnitOfWork _unitOfWork;

        public BaseRepository(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        /// <summary>
        /// Tạo một Entity
        /// </summary>
        /// <param name="tEntityInput"></param>
        /// <returns>Giá trị boolean biểu thị entity được tạo thành công hay chưa</returns>
        /// Author: DNT(20/05/2023)
        public async Task<bool> CreateAsync(TEntityInput tEntityInput)
        {
            try
            {
                var dynamicParams = new DynamicParameters();

                foreach (var property in typeof(TEntityInput).GetProperties())
                {
                    var propertyNameToCamelCase = char.ToLower(property.Name[0]) + property.Name[1..];
                    var paramName = "p_" + propertyNameToCamelCase;
                    var paramValue = property.GetValue(tEntityInput);
                    dynamicParams.Add(paramName, paramValue);
                } 
                var entityClassName = typeof(TEntity).Name;
                
                var rowAffected = await _unitOfWork.Connection.ExecuteAsync(StoredProcedureName.GetProcedureNameByEntityClassName(entityClassName + "Create"), commandType: CommandType.StoredProcedure, param: dynamicParams, transaction: _unitOfWork.Transaction);

                return (rowAffected != 0);

            } catch (Exception ex)
            {
                throw new DbException(Error.DbQueryFail, ex.Message, Error.DbQueryFailMsg);
            }
        }

        /// <summary>
        /// Lấy ra một Entity theo ID
        /// </summary>
        /// <param name="id">ID của Entity</param>
        /// <returns>Trả về một Entity</returns>
        /// Author: DNT(20/05/2023)
        public async Task<TEntity?> GetAsync(Guid id)
        {
            try
            {
                var entityClassName = typeof(TEntity).Name;

                var dynamicParams = new DynamicParameters();
                dynamicParams.Add("p_id", id);


                var entity = await _unitOfWork.Connection.QueryFirstOrDefaultAsync<TEntity?>(StoredProcedureName.GetProcedureNameByEntityClassName(entityClassName),
                    commandType: CommandType.StoredProcedure, param: dynamicParams, transaction: _unitOfWork.Transaction);

                return entity;
            } catch (Exception ex)
            {
                throw new DbException(Error.DbQueryFail, ex.Message, Error.DbQueryFailMsg);
            }
   
        }

        /// <summary>
        /// Filter danh sách Entity
        /// </summary>
        /// <param name="entityFilter"></param>
        /// <returns>Filtered List lưu danh sách các entity tìm được</returns>
        /// <exception cref="DbException"></exception>
        /// Author: DNT(29/05/2023)
        public async Task<FilteredList<TEntity>> FilterAsync(EntityFilter entityFilter)
        {
            try
            {
                var dynamicParams = new DynamicParameters();

                var entityName = typeof(TEntity).Name;
                var storedProcedureKey = $"FilteredList<{entityName}>";

                var proceduredName = StoredProcedureName.GetProcedureNameByEntityClassName(storedProcedureKey);

                dynamicParams.Add("p_skip", entityFilter.Skip);
                dynamicParams.Add("p_take", entityFilter.Take);
                dynamicParams.Add("p_keySearch", entityFilter.KeySearch);
                dynamicParams.Add("o_totalRecord", direction: ParameterDirection.Output);

                var listData = await _unitOfWork.Connection.QueryAsync<TEntity?>(proceduredName,
                    commandType: CommandType.StoredProcedure, param: dynamicParams, transaction: _unitOfWork.Transaction);
                var totalRecord = dynamicParams.Get<int>("o_totalRecord");

                FilteredList<TEntity> filteredList = new()
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

        /// <summary>
        /// Cập nhật thông tin một Entity theo ID
        /// </summary>
        /// <param name="tEntityInput"></param>
        /// <returns>Giá trị boolean biểu thị cập nhật thành công hay chưa</returns>
        /// Author: DNT(20/05/2023)
        public virtual async Task<bool> UpdateAsync(TEntityInput tEntityInput)
        {

            try
            {
                var entityName = typeof(TEntity).Name;

                var queryString = $"UPDATE {entityName} SET ";
                
                foreach (var property in typeof(TEntityInput).GetProperties())
                {
                    var paramValue = property.GetValue(tEntityInput);
                    if (paramValue != null && property.Name != $"{entityName}Id")
                    {
                        queryString += $"{property.Name} = @{property.Name} ,";
                    }
                        
                }
               
                queryString = queryString.Remove(queryString.Length - 1);
                queryString += $"WHERE {entityName}Id = @{entityName}Id;";
                
                var rowAffected = await _unitOfWork.Connection.ExecuteAsync(queryString, tEntityInput, transaction: _unitOfWork.Transaction);
                return (rowAffected != 0);
            }
            catch (Exception ex)
            {
                throw new DbException(Error.DbQueryFail, ex.Message, Error.DbQueryFailMsg);
            }
        }

        /// <summary>
        /// Xóa một Entity theo ID
        /// </summary>
        /// <param name="id">ID của entity</param>
        /// <returns>Giá trị boolean biểu thị xóa thành công hay chưa</returns>
        /// Author: DNT(20/05/2023)
        public async Task<bool> DeleteByIdAsync(Guid id)
        {
            try
            {
                var dynamicParams = new DynamicParameters();

                var entityClassName = typeof(TEntity).Name;
                var storedProcedureKey = entityClassName + "Delete";
                dynamicParams.Add("p_id", id);
                
                var rowAffected = await _unitOfWork.Connection.ExecuteAsync(StoredProcedureName.GetProcedureNameByEntityClassName(storedProcedureKey), commandType: CommandType.StoredProcedure, param: dynamicParams, transaction: _unitOfWork.Transaction);
                return (rowAffected != 0);
            } catch(Exception ex)
            {
                throw new DbException(Error.DbQueryFail, ex.Message, Error.DbQueryFailMsg);
            }
        }

        /// <summary>
        /// Xóa hàng loạt entity theo danh sách ID
        /// </summary>
        /// <param name="stringIdList">Danh sách ID được nối với nhau ngăn cách bởi dấu phẩy</param>
        /// <returns>Số lượng Entity đã xóa thành công</returns>
        /// <exception cref="NotImplementedException"></exception>
        /// Author: DNT(26/05/2023)
        public async Task<int> DeleteMultipleAsync(string stringIdList)
        {
            try
            {
                var dynamicParams = new DynamicParameters();
                dynamicParams.Add("p_idList", stringIdList);
               
                var entityClassName = typeof(TEntity).Name;
                var storedProcedureKey = entityClassName + "DeleteMultiple";

                var proceduredName = StoredProcedureName.GetProcedureNameByEntityClassName(storedProcedureKey);
                
                var rowAffected = await _unitOfWork.Connection.ExecuteAsync(proceduredName, commandType: CommandType.StoredProcedure, param: dynamicParams, transaction: _unitOfWork.Transaction);
                return rowAffected;
            }
            catch (Exception ex)
            {   
                throw new DbException(Error.DbQueryFail, ex.Message, Error.DbQueryFailMsg);
            }
        }

        /// <summary>
        /// Kiểm tra mã  đã tồn tại
        /// </summary>
        /// <param name="id">ID của entity (chỉ dùng trong trường hợp update)</param>
        /// <param name="code">Mã</param>
        /// <returns>Giá trị boolean biểu thị mã có tồn tại hay không</returns>
        /// Author: DNT(20/05/2023)
        public async Task<bool> CheckCodeExistAsync(Guid? id, string code)
        {
            try
            {
                var dynamicParams = new DynamicParameters();
                dynamicParams.Add("p_id", id);
                dynamicParams.Add("p_code", code);
                dynamicParams.Add("o_isExist", direction: ParameterDirection.Output);

                var entityClassName = typeof(TEntity).Name;
                var storedProcedureKey = entityClassName + "CheckCodeExist";

                var proceduredName = StoredProcedureName.GetProcedureNameByEntityClassName(storedProcedureKey);

                await _unitOfWork.Connection.ExecuteAsync(proceduredName, commandType: CommandType.StoredProcedure, param: dynamicParams, transaction: _unitOfWork.Transaction);
                var isExist = dynamicParams.Get<bool>("o_isExist");
                return isExist;
            }
            catch (Exception ex)
            {
                throw new DbException(Error.DbQueryFail, ex.Message, Error.DbQueryFailMsg);
            }
        }
    }
}
