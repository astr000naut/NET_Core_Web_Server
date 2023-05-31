﻿using Dapper;
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
        /// Author: DNT(20/05/2023)
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
            var createTransaction = await connection.BeginTransactionAsync();
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
                var rowAffected = await connection.ExecuteAsync(StoredProcedureName.GetProcedureNameByEntityClassName(entityClassName), commandType: CommandType.StoredProcedure, param: dynamicParams, transaction: createTransaction);
                await createTransaction.CommitAsync();

                return (rowAffected != 0);

            } catch (Exception ex)
            {
                await createTransaction.RollbackAsync();
                throw new DbException(Error.DbQueryFail, ex.Message, Error.DbQueryFailMsg);
            }
            finally
            {
                await createTransaction.DisposeAsync();
                await connection.CloseAsync();
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

                return entity;
            } catch (Exception ex)
            {
               
                throw new DbException(Error.DbQueryFail, ex.Message, Error.DbQueryFailMsg);
            } finally
            {
                await connection.CloseAsync();
            }
   
        }

        /// <summary>
        /// Filter danh sách Entity
        /// </summary>
        /// <param name="entityFilter"></param>
        /// <returns></returns>
        /// <exception cref="DbException"></exception>
        /// Author: DNT(29/05/2023)
        public async Task<FilteredList<TEntity>> FilterAsync(EntityFilter entityFilter)
        {
            var connection = await GetOpenConnectionAsync();
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

                var listData = await connection.QueryAsync<TEntity?>(proceduredName,
                    commandType: CommandType.StoredProcedure, param: dynamicParams);
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
            } finally
            {
                await connection.CloseAsync();
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
            var updateTransaction = await connection.BeginTransactionAsync();
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
                var rowAffected = await connection.ExecuteAsync(StoredProcedureName.GetProcedureNameByEntityClassName(entityClassName), commandType: CommandType.StoredProcedure, param: dynamicParams, transaction: updateTransaction);
                await updateTransaction.CommitAsync();
                return (rowAffected != 0);
            }
            catch (Exception ex)
            {
                await updateTransaction.RollbackAsync();
                throw new DbException(Error.DbQueryFail, ex.Message, Error.DbQueryFailMsg);
            }
            finally
            {
                await updateTransaction.DisposeAsync();
                await connection.CloseAsync();
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
            var deleteTransaction = await connection.BeginTransactionAsync();
            try
            {
                var dynamicParams = new DynamicParameters();

                var entityClassName = typeof(TEntity).Name;
                var storedProcedureKey = entityClassName + "Delete";
                dynamicParams.Add("p_id", id);
                
                var rowAffected = await connection.ExecuteAsync(StoredProcedureName.GetProcedureNameByEntityClassName(storedProcedureKey), commandType: CommandType.StoredProcedure, param: dynamicParams, transaction: deleteTransaction);
                await deleteTransaction.CommitAsync();
                return (rowAffected != 0);
            } catch(Exception ex)
            {
                await deleteTransaction.RollbackAsync();
                throw new DbException(Error.DbQueryFail, ex.Message, Error.DbQueryFailMsg);
            } finally
            {
                await deleteTransaction.DisposeAsync(); 
                await connection.CloseAsync();
            }
        }

        /// <summary>
        /// Xoa hang loat
        /// </summary>
        /// <param name="stringIdList"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        /// Author: DNT(26/05/2023)
        public async Task<int> DeleteMultipleAsync(string stringIdList)
        {
            var connection = await GetOpenConnectionAsync();
            var deleteTransaction = await connection.BeginTransactionAsync();
            try
            {
                var dynamicParams = new DynamicParameters();
                dynamicParams.Add("p_idList", stringIdList);
               
                var entityClassName = typeof(TEntity).Name;
                var storedProcedureKey = entityClassName + "DeleteMultiple";

                var proceduredName = StoredProcedureName.GetProcedureNameByEntityClassName(storedProcedureKey);
                
                var rowAffected = await connection.ExecuteAsync(proceduredName, commandType: CommandType.StoredProcedure, param: dynamicParams, transaction: deleteTransaction);
                await deleteTransaction.CommitAsync();
                return rowAffected;
            }
            catch (Exception ex)
            {
                await deleteTransaction.RollbackAsync();
                throw new DbException(Error.DbQueryFail, ex.Message, Error.DbQueryFailMsg);
            } finally
            {
                await deleteTransaction.DisposeAsync();
                await connection.CloseAsync();
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
                return isExist;
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
