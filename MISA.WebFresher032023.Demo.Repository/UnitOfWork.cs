using Microsoft.Extensions.Configuration;
using MISA.WebFresher032023.Demo.Common.Enums;
using MISA.WebFresher032023.Demo.Common.Exceptions;
using MISA.WebFresher032023.Demo.DataLayer.Repositories;
using MISA.WebFresher032023.Demo.DataLayer.Repositories.AccountRepo;
using MISA.WebFresher032023.Demo.DataLayer.Repositories.GroupRepo;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DbException = MISA.WebFresher032023.Demo.Common.Exceptions.DbException;

namespace MISA.WebFresher032023.Demo.DataLayer
{
    public class UnitOfWork : IUnitOfWork
    {
        private DbConnection _connection;
        private DbTransaction _transaction;
        private Guid? _manipulationKey;

        public UnitOfWork(IConfiguration configuration)
        {
            string connectionString = configuration.GetConnectionString("SqlConnection") ?? "";
            _connection = new MySqlConnection(connectionString);
            _manipulationKey = null;
        }

        public void setManipulationKey(Guid key)
        {
            if (_manipulationKey == null) 
                _manipulationKey = key;
        }

        public DbConnection Connection => _connection;

        public DbTransaction Transaction => _transaction;

        public void Begin(Guid key)
        {
            if (key == _manipulationKey)
            {
                _transaction = _connection.BeginTransaction();
            }
        }

        public async Task BeginAsync(Guid key)
        {
            if (key == _manipulationKey)
            {
                _transaction = await _connection.BeginTransactionAsync();
            }
        }

        public void Commit(Guid key)
        {
            if (key == _manipulationKey)
            {
                _transaction.Commit();
            }
        }

        public async Task CommitAsync(Guid key)
        {
            if (key == _manipulationKey)
            {
                await _transaction.CommitAsync();
            }
        }


        public async Task DisposeAsync(Guid key)
        {
            if (key == _manipulationKey)
            {
                if (_transaction != null)
                    await _transaction.DisposeAsync();

                _transaction = null;
            }
        }

        public async Task OpenAsync(Guid key)
        {
            if (key == _manipulationKey)
            {
                await _connection.OpenAsync();
            }
        }

        public async Task CloseAsync(Guid key)
        {
            if (key == _manipulationKey)
            {
                await _connection.CloseAsync();
                _manipulationKey = null;
            }
        }


        public void Rollback(Guid key)
        {
            if (key == _manipulationKey)
            {
                _transaction.Rollback();
            }
        }

        public async Task RollbackAsync(Guid key)
        {
            if (key == _manipulationKey)
            {
                await _transaction.RollbackAsync();
            }
        }

        public void Dispose()
        {
            if (_transaction != null)
                _transaction.Dispose();

            _transaction = null;
        }
    }
}
