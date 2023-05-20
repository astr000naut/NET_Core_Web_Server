using AutoMapper;
using MISA.WebFresher032023.Demo.BusinessLayer.Dtos.Input;
using MISA.WebFresher032023.Demo.BusinessLayer.Dtos.Output;
using MISA.WebFresher032023.Demo.DataLayer.Entities.Input;
using MISA.WebFresher032023.Demo.DataLayer.Entities.Output;
using MISA.WebFresher032023.Demo.DataLayer.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WebFresher032023.Demo.BusinessLayer.Services
{
    public class EmployeeService : BaseService<Employee, EmployeeDto, EmployeeCreate, EmployeeCreateDto, EmployeeUpdate, EmployeeUpdateDto>, IEmployeeService
    {
        private readonly IEmployeeRepository _employeeRepository;

        public EmployeeService(IEmployeeRepository employeeRepository, IMapper mapper) : base(employeeRepository, mapper) {
            _employeeRepository = employeeRepository;
        }
   
       
        public async Task<string> GetNewCodeAsync()
        {
            var newCode = await _employeeRepository.GetNewCodeAsync();
            return newCode;
        }
       
        public override async Task<Guid?> CreateAsync(EmployeeCreateDto employeeCreateDto)
        {
            // Validate

            // Create
           /* var employeeCreate = _mapper.Map<EmployeeCreate>(employeeCreateDto);
            employeeCreate.EmployeeId = new Guid();
            employeeCreate.CreatedDate = DateTime.Now.ToLocalTime();
            employeeCreate.CreatedBy = "Dux";
            employeeCreate.ModifiedDate = DateTime.Now.ToLocalTime();
            employeeCreate.ModifiedBy = "Dux";
            await _employeeRepository.CreateAsync(employeeCreate);

            return employeeCreate.EmployeeId;*/
           throw new NotImplementedException();
        }
       
        public override async Task UpdateAsync(Guid id, EmployeeUpdateDto employeeUpdateDto)
        {
            // Validate

            // Update
            /*var employeeUpdate = _mapper.Map<EmployeeUpdate>(employeeUpdateDto);
            employeeUpdate.ModifiedDate = DateTime.Now.ToLocalTime();
            employeeUpdate.ModifiedBy = "DUX";
            await _employeeRepository.UpdateAsync(id, employeeUpdate);*/
            throw new NotImplementedException();
        }
    }
}
