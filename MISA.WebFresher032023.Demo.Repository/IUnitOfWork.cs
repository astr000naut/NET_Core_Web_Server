using MISA.WebFresher032023.Demo.DataLayer.Repositories;
using MISA.WebFresher032023.Demo.DataLayer.Repositories.AccountRepo;
using MISA.WebFresher032023.Demo.DataLayer.Repositories.GroupRepo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace MISA.WebFresher032023.Demo.DataLayer
{
    public interface IUnitOfWork:IDisposable
    {
        void Commit();
        void ResetRepositories();
        AccountRepository AccountRepository { get; }
        CustomerRepository CustomerRepository { get; }
        DepartmentRepository DepartmentRepository { get; }
        EmployeeRepository EmployeeRepository { get; }
        GroupRepository GroupRepository { get; }
    }
}
