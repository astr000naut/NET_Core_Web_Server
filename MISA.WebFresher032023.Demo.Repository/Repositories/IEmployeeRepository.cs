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
        /// <summary>
        /// Kiểm tra mã nhân viên có tồn tại hay không
        /// </summary>
        /// <param name="id"></param>
        /// <param name="employeeCode"></param>
        /// <returns></returns>
        /// Author: DNT(20/05/2023)
        Task<Boolean> CheckCodeExistAsync(Guid? id, string employeeCode);

        /// <summary>
        /// Lấy mã nhân viên mới
        /// </summary>
        /// <returns></returns>
        /// /// Author: DNT(20/05/2023)
        Task<string> GetNewCodeAsync();

        /// <summary>
        /// Kiểm tra ID của đơn vị khi tạo mới nhân viên
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<bool> ValidateDepartmentId(Guid? id);

    }
}
