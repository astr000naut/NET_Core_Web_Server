using MISA.WebFresher032023.Demo.BusinessLayer.Dtos.Input;
using MISA.WebFresher032023.Demo.BusinessLayer.Dtos.Output;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WebFresher032023.Demo.BusinessLayer.Services
{
    public interface IEmployeeService : IBaseService<EmployeeDto,EmployeeInputDto>
    {
        /// <summary>
        /// Sinh mã nhân viên mới
        /// </summary>
        /// <returns>Mã nhân viên mới</returns>
        Task<string> GetNewCodeAsync();

        /// <summary>
        /// Xuất file excel thông tin của nhân viên
        /// </summary>
        /// <returns>Woorkbook thông tin nhân viên được chuyển sang mảng byte[]</returns>
        Task<byte[]> ExportEmployeesToExcelAsync();
    }
}
