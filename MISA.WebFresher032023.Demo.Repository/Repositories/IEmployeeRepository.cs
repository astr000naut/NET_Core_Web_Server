using MISA.WebFresher032023.Demo.DataLayer.Entities.Input;
using MISA.WebFresher032023.Demo.DataLayer.Entities.Output;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WebFresher032023.Demo.DataLayer.Repositories
{
    public interface IEmployeeRepository
    {
        Task<DbConnection> GetOpenConnectionAsync();

        Task<Boolean> CheckCodeExistAsync(Guid? id, string employeeCode);

        Task DeleteByIdAsync(Guid id);

        Task<EmployeeFilteredList> FilterAsync(int skip, int take, string? employeeSearch);

        Task<string> GetNewCodeAsync();

        Task<Employee?> GetAsync(Guid employeeId);

        Task CreateAsync(EmployeeCreate employeeCreate);

        Task UpdateAsync(Guid employeeId, EmployeeUpdate employeeUpdate);
        
    }
}
