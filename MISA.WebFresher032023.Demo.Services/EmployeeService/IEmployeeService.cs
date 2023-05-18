using MISA.WebFresher032023.Demo.BusinessLayer.EmployeeService.Dto.Input;
using MISA.WebFresher032023.Demo.BusinessLayer.EmployeeService.Dto.Output;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WebFresher032023.Demo.BusinessLayer.EmployeeService
{
    public interface IEmployeeService
    {
        Task DeleteByIdAsync(Guid id);
        Task<EmployeeFilteredListDto> FilterAsync(int skip, int take, string? employeeSearch);
        Task<string> GetNewCodeAsync();
        Task<EmployeeDto?> GetAsync(Guid id);
        Task<Guid?> CreateAsync(EmployeeCreateDto employeeCreateDto);
        Task UpdateAsync(Guid id, EmployeeUpdateDto employeeUpdateDto);
        
    }
}
