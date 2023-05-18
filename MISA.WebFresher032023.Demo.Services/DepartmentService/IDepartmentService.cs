using MISA.WebFresher032023.Demo.BusinessLayer.DepartmentService.Dto.FromClient;
using MISA.WebFresher032023.Demo.BusinessLayer.DepartmentService.Dto.Output;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WebFresher032023.Demo.BusinessLayer.DepartmentService
{
    public interface IDepartmentService
    {
        Task DeleteByIdAsync(Guid id);
        Task<DepartmentDto?> GetAsync(Guid id);
        Task<IEnumerable<DepartmentDto?>> GetAllAsync();
        Task CreateAsync(DepartmentCreateDto departmentCreateDto);
        Task UpdateAsync(Guid departmentId, DepartmentUpdateDto departmentUpdateDto);
    }
}
