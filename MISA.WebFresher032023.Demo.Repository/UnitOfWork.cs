using Microsoft.Extensions.Configuration;
using MISA.WebFresher032023.Demo.Common.Configs;
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
        private int _manipulationKey;

        public UnitOfWork(IConfiguration configuration)
        {
            string connectionString = configuration.GetConnectionString("SqlConnection") ?? "";
            _connection = new MySqlConnection(connectionString);
            _manipulationKey = 0;
        }

        public void SetManipulationKey(int key)
        {
            _manipulationKey = key;
        }

        public int GetManipulationKey() { return _manipulationKey; }

        public DbConnection Connection => _connection;

        public DbTransaction Transaction => _transaction;

        public void Begin(int key)
        {
            if (key == 0)
            {
                _transaction = _connection.BeginTransaction();
            }
        }

        public async Task BeginAsync(int key)
        {
            if (key == 0)
            {
                _transaction = await _connection.BeginTransactionAsync();
            }
        }

        public void Commit(int key)
        {
            if (key == 0)
            {
                _transaction.Commit();
            }
        }

        public async Task CommitAsync(int key)
        {
            if (key == 0)
            {
                await _transaction.CommitAsync();
            }
        }


        public async Task DisposeAsync(int key)
        {
            if (key == 0)
            {
                if (_transaction != null)
                    await _transaction.DisposeAsync();

                _transaction = null;
            }
        }

        public async Task OpenAsync(int key)
        {
            if (key == 0)
            {
                await _connection.OpenAsync();
            }
        }

        public async Task CloseAsync(int key)
        {
            if (key == 0)
            {
                await _connection.CloseAsync();
            }
        }


        public void Rollback(int key)
        {
            if (key == 0)
            {
                _transaction.Rollback();
            }
        }

        public async Task RollbackAsync(int key)
        {
            if (key == 0)
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
