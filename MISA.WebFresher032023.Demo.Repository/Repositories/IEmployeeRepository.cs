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
    public interface IEmployeeRepository : IBaseRepository<Employee, EmployeeCreate, EmployeeUpdate>
    {

        Task<Boolean> CheckCodeExistAsync(Guid? id, string employeeCode);

        Task<string> GetNewCodeAsync();

    }
}
