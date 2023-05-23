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
    public abstract class BaseRepository<TEntity, TEntityCreate, TEntityUpdate> 
        : IBaseRepository<TEntity, TEntityCreate, TEntityUpdate>

    {
        private readonly string _connectionString;

        /// <summary>
        /// Hàm khởi tạo
        /// </summary>
        /// <param name="configuration"></param>
        public BaseRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("SqlConnection") ?? "";
        }


        /// <summary>
        /// Khởi tạo kết nối tới DB
        /// </summary>
        /// <returns></returns>
        /// Author: DNT(20/05/2023)
        public async Task<DbConnection> GetOpenConnectionAsync()
        {
            var connection = new MySqlConnection(_connectionString);
            try
            {
                await connection.OpenAsync();
                return connection;
            } catch (Exception ex)
            {
                await connection.CloseAsync();
                throw new DbException(Error.DbConnectFail, ex.Message, Error.DbConnectFailMessage);
            }
        }

        /// <summary>
        /// Tạo một Entity
        /// </summary>
        /// <param name="tEntityCreate"></param>
        /// <returns></returns>
        /// Author: DNT(20/05/2023)
        public async Task<bool> CreateAsync(TEntityCreate tEntityCreate)
        {
            var connection = await GetOpenConnectionAsync();
            try
            {
                var dynamicParams = new DynamicParameters();

                foreach (var property in typeof(TEntityCreate).GetProperties())
                {
                    var propertyNameToCamelCase = char.ToLower(property.Name[0]) + property.Name[1..];
                    var paramName = "p_" + propertyNameToCamelCase;
                    var paramValue = property.GetValue(tEntityCreate);
                    dynamicParams.Add(paramName, paramValue);
                }
                var entityClassName = typeof(TEntityCreate).Name;
                var rowAffected = await connection.ExecuteAsync(StoredProcedureName.GetProcedureNameByEntityClassName(entityClassName), commandType: CommandType.StoredProcedure, param: dynamicParams);
                await connection.CloseAsync();
                return (rowAffected != 0);
            } catch (Exception ex)
            {
                await connection.CloseAsync();
                throw new DbException(Error.DbQueryFail, ex.Message, Error.DbQueryFailMsg);
            }
        }

        /// <summary>
        /// Lấy ra một Entity theo ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// Author: DNT(20/05/2023)
        public async Task<TEntity?> GetAsync(Guid id)
        {
            var connection = await GetOpenConnectionAsync();
            
            try
            {
                var entityClassName = typeof(TEntity).Name;

                var dynamicParams = new DynamicParameters();
                dynamicParams.Add("p_id", id);


                var entity = await connection.QueryFirstOrDefaultAsync<TEntity?>(StoredProcedureName.GetProcedureNameByEntityClassName(entityClassName),
                    commandType: CommandType.StoredProcedure, param: dynamicParams);

                await connection.CloseAsync();
                return entity;
            } catch (Exception ex)
            {
                await connection.CloseAsync();
                throw new DbException(Error.DbQueryFail, ex.Message, Error.DbQueryFailMsg);
            }
   
        }

        /// <summary>
        /// Filter Entity
        /// </summary>
        /// <param name="skip"></param>
        /// <param name="take"></param>
        /// <param name="keySearch"></param>
        /// <returns></returns>
        /// Author: DNT(20/05/2023)
        public async Task<FilteredList<TEntity>> FilterAsync(int skip, int? take, string keySearch)
        {
            var connection = await GetOpenConnectionAsync();
            try
            {
                var dynamicParams = new DynamicParameters();

                var entityName = typeof(TEntity).Name;
                var storedProcedureKey = $"FilteredList<{entityName}>";

                var proceduredName = StoredProcedureName.GetProcedureNameByEntityClassName(storedProcedureKey);

                dynamicParams.Add("p_skip", skip);
                dynamicParams.Add("p_take", take);
                dynamicParams.Add("p_keySearch", keySearch);
                dynamicParams.Add("o_totalRecord", direction: ParameterDirection.Output);

                var listData = await connection.QueryAsync<TEntity?>(proceduredName,
                    commandType: CommandType.StoredProcedure, param: dynamicParams);
                var totalRecord = dynamicParams.Get<int>("o_totalRecord");

                FilteredList<TEntity> filteredList = new()
                {
                    ListData = listData,
                    TotalRecord = totalRecord
                };
                await connection.CloseAsync();
                return filteredList;
            }
            catch (Exception ex)
            {
                await connection.CloseAsync();
                throw new DbException(Error.DbQueryFail, ex.Message, Error.DbQueryFailMsg);
            }       
        }

        /// <summary>
        /// Cập nhật thông tin một Entity theo ID
        /// </summary>
        /// <param name="id"></param>
        /// <param name="tEntityUpdate"></param>
        /// <returns></returns>
        /// Author: DNT(20/05/2023)
        public async Task<bool> UpdateAsync(Guid id, TEntityUpdate tEntityUpdate)
        {
            var connection = await GetOpenConnectionAsync();
            try
            {
                var dynamicParams = new DynamicParameters();

                foreach (var property in typeof(TEntityUpdate).GetProperties())
                {
                    var propertyNameToCamelCase = char.ToLower(property.Name[0]) + property.Name[1..];
                    var paramName = "p_" + propertyNameToCamelCase;
                    var paramValue = property.GetValue(tEntityUpdate);
                    dynamicParams.Add(paramName, paramValue);
                }
                var entityClassName = typeof(TEntityUpdate).Name;
                var rowAffected = await connection.ExecuteAsync(StoredProcedureName.GetProcedureNameByEntityClassName(entityClassName), commandType: CommandType.StoredProcedure, param: dynamicParams);
                await connection.CloseAsync();
                return (rowAffected != 0);
            } catch (Exception ex)
            {
                await connection.CloseAsync();
                throw new DbException(Error.DbQueryFail, ex.Message, Error.DbQueryFailMsg);
            }
        }

        /// <summary>
        /// Xóa một Entity theo ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// Author: DNT(20/05/2023)
        public async Task<bool> DeleteByIdAsync(Guid id)
        {
            var connection = await GetOpenConnectionAsync();
            try
            {
                var dynamicParams = new DynamicParameters();

                var entityClassName = typeof(TEntity).Name;
                var storedProcedureKey = entityClassName + "Delete";
                dynamicParams.Add("p_id", id);
                var rowAffected = await connection.ExecuteAsync(StoredProcedureName.GetProcedureNameByEntityClassName(storedProcedureKey), commandType: CommandType.StoredProcedure, param: dynamicParams);
                await connection.CloseAsync();
                return (rowAffected != 0);
            } catch(Exception ex)
            {
                await connection.CloseAsync();
                throw new DbException(Error.DbQueryFail, ex.Message, Error.DbQueryFailMsg);
            }
        }

        /// <summary>
        /// Kiểm tra mã  đã tồn tại
        /// </summary>
        /// <param name="id"></param>
        /// <param name="code"></param>
        /// <returns></returns>
        /// Author: DNT(20/05/2023)
        public async Task<bool> CheckCodeExistAsync(Guid? id, string code)
        {
            var connection = await GetOpenConnectionAsync();
            try
            {
                var dynamicParams = new DynamicParameters();
                dynamicParams.Add("p_id", id);
                dynamicParams.Add("p_code", code);
                dynamicParams.Add("o_isExist", direction: ParameterDirection.Output);

                var entityClassName = typeof(TEntity).Name;
                var storedProcedureKey = entityClassName + "CheckCodeExist";

                var proceduredName = StoredProcedureName.GetProcedureNameByEntityClassName(storedProcedureKey);

                await connection.ExecuteAsync(proceduredName, commandType: CommandType.StoredProcedure, param: dynamicParams);
                var isExist = dynamicParams.Get<bool>("o_isExist");
                await connection.CloseAsync();
                return isExist;
            }
            catch (Exception ex)
            {
                await connection.CloseAsync();
                throw new DbException(Error.DbQueryFail, ex.Message, Error.DbQueryFailMsg);
            }
        }
    }
}
