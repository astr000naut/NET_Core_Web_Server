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
        private IDbConnection _connection;
        private IDbTransaction _transaction;

        private AccountRepository _accountRepository;
        private CustomerRepository _customerRepository;
        private DepartmentRepository _departmentRepository;
        private EmployeeRepository  _employeeRepository;
        private GroupRepository _groupRepository;

        public UnitOfWork(IConfiguration configuration)
        {
            string connectionString = configuration.GetConnectionString("SqlConnection") ?? "";
            _connection = new MySqlConnection(connectionString);
            _connection.Open();
            _transaction = _connection.BeginTransaction();
        }

        public void Commit()
        {
            try
            {
                _transaction.Commit();
            }
            catch(Exception ex)
            {
                _transaction.Rollback();
                throw new DbException(Error.DbConnectFail, ex.Message, Error.DbConnectFailMsg);
            }
            finally {
                _transaction.Dispose();
                _transaction = _connection.BeginTransaction();
                ResetRepositories();
            }
        }

        public void Dispose()
        {
            if (_transaction != null)
            {
                _transaction.Dispose();
                _transaction = null;
            }

            if (_connection != null)
            {
                _connection.Dispose();
                _connection = null;
            }
        }

        public void ResetRepositories()
        {
            _accountRepository = null;
            _customerRepository = null;
            _departmentRepository = null;
            _employeeRepository = null;
            _groupRepository = null;
        }




        public AccountRepository AccountRepository
        {
            get
            {
                return _accountRepository ??= new AccountRepository(_transaction);
            }
        }

        public CustomerRepository CustomerRepository
        {
            get
            {
                return _customerRepository ??= new CustomerRepository(_transaction);
            }
        }

        public DepartmentRepository DepartmentRepository
        {
            get
            {
                return _departmentRepository ??= new DepartmentRepository(_transaction);
            }
        }

        public EmployeeRepository EmployeeRepository
        {
            get
            {
                return _employeeRepository ??= new EmployeeRepository(_transaction);
            }
        }

        public GroupRepository GroupRepository
        {
            get
            {
                return _groupRepository ??= new GroupRepository(_transaction);
            }
        }
    }
}
