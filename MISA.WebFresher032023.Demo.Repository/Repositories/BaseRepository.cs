using Dapper;
using Microsoft.Extensions.Configuration;
using MISA.WebFresher032023.Demo.Common.Enum;
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

namespace MISA.WebFresher032023.Demo.DataLayer.Repositories
{
    public abstract class BaseRepository<TEntity, TEntityCreate, TEntityUpdate> 
        : IBaseRepository<TEntity, TEntityCreate, TEntityUpdate>

    {
        private readonly string _connectionString;
        /// <summary>
        /// Hàm khởi tạo - DONE
        /// </summary>
        /// <param name="configuration">Inject configuration</param>
        public BaseRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("SqlConnection") ?? "";
        }

        public async Task<DbConnection> GetOpenConnectionAsync()
        {
            var connection = new MySqlConnection(_connectionString);
            await connection.OpenAsync();
            return connection;
        }

        public async Task CreateAsync(TEntityCreate tEntityCreate)
        {
            var connection = await GetOpenConnectionAsync();
            var dynamicParams = new DynamicParameters();

            foreach (var property in typeof(TEntityCreate).GetProperties())
            {
                var propertyNameToCamelCase = char.ToLower(property.Name[0]) + property.Name.Substring(1);
                var paramName = "p_" + propertyNameToCamelCase;
                var paramValue = property.GetValue(tEntityCreate);
                dynamicParams.Add(paramName, paramValue);
            }
            var entityClassName = typeof(TEntityCreate).Name;
            await connection.ExecuteAsync(StoredProcedureName.GetProcedureNameByEntityClassName(entityClassName), commandType: CommandType.StoredProcedure, param: dynamicParams);
            await connection.CloseAsync();
        }

        public async Task<TEntity?> GetAsync(Guid id)
        {
            var connection = await GetOpenConnectionAsync();
            

            var entityClassName = typeof(TEntity).Name;

            var dynamicParams = new DynamicParameters();
            dynamicParams.Add("p_id", id);

            
            var entity = await connection.QueryFirstOrDefaultAsync<TEntity?>(StoredProcedureName.GetProcedureNameByEntityClassName(entityClassName), 
                commandType: CommandType.StoredProcedure, param: dynamicParams);
            await connection.CloseAsync();
            return entity;
        }

        public async Task<FilteredList<TEntity>> FilterAsync(int skip, int take, string keySearch)
        {
            var connection = await GetOpenConnectionAsync();
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

        public async Task UpdateAsync(Guid id, TEntityUpdate tEntityUpdate)
        {
            var connection = await GetOpenConnectionAsync();
            var dynamicParams = new DynamicParameters();

            foreach (var property in typeof(TEntityUpdate).GetProperties())
            {
                var propertyNameToCamelCase = char.ToLower(property.Name[0]) + property.Name[1..];
                var paramName = "p_" + propertyNameToCamelCase;
                var paramValue = property.GetValue(tEntityUpdate);
                dynamicParams.Add(paramName, paramValue);
            }
            var entityClassName = typeof(TEntityUpdate).Name;
            await connection.ExecuteAsync(StoredProcedureName.GetProcedureNameByEntityClassName(entityClassName), commandType: CommandType.StoredProcedure, param: dynamicParams);
            await connection.CloseAsync();
        }

        public async Task DeleteByIdAsync(Guid id)
        {
            var connection = await GetOpenConnectionAsync();
            var dynamicParams = new DynamicParameters();

            var entityClassName = typeof(TEntity).Name;
            var storedProcedureKey = entityClassName + "Delete";
            dynamicParams.Add("p_id", id);
            await connection.ExecuteAsync(StoredProcedureName.GetProcedureNameByEntityClassName(storedProcedureKey), commandType: CommandType.StoredProcedure, param: dynamicParams);
            await connection.CloseAsync();
        }

    }
}
