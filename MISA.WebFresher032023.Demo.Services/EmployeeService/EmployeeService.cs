using AutoMapper;
using MISA.WebFresher032023.Demo.BusinessLayer.EmployeeService.Dto.Input;
using MISA.WebFresher032023.Demo.BusinessLayer.EmployeeService.Dto.Output;
using MISA.WebFresher032023.Demo.DataLayer.Entities.Input;
using MISA.WebFresher032023.Demo.DataLayer.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WebFresher032023.Demo.BusinessLayer.EmployeeService
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IMapper _mapper;

        public EmployeeService(IEmployeeRepository employeeRepository, IMapper mapper)
        {
            _employeeRepository = employeeRepository;
            _mapper = mapper;
        }

        /// <summary>
        /// Xóa nhân viên theo ID - DONE
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task DeleteByIdAsync(Guid id)
        {
            await _employeeRepository.DeleteByIdAsync(id);
        }

        /// <summary>
        /// Filter nhân viên - DONE
        /// </summary>
        /// <param name="skip"></param>
        /// <param name="take"></param>
        /// <param name="employeeSearch"></param>
        /// <returns></returns>
        public async Task<EmployeeFilteredListDto> FilterAsync(int skip, int take, string? employeeSearch)
        {
            var employeeFilteredList = await _employeeRepository.FilterAsync(skip, take, employeeSearch);
            var employeeFilteredListDto = new EmployeeFilteredListDto
            {
                TotalRecord = employeeFilteredList.TotalRecord, 
                FilteredList = new List<EmployeeDto>()
        };

            foreach (var employee in employeeFilteredList.FilteredList)
            {
                var employeeDto = _mapper.Map<EmployeeDto>(employee);
                employeeFilteredListDto.FilteredList.Add(employeeDto);
            }
      
            return employeeFilteredListDto;
        }
        /// <summary>
        /// Lấy nhân viên theo ID - DONE
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<EmployeeDto?> GetAsync(Guid id)
        {
            var employee = await _employeeRepository.GetAsync(id);
            
            if (employee == null) { return null; }
            var employeeDto = _mapper.Map<EmployeeDto>(employee);
            return employeeDto;
        }
        /// <summary>
        /// Lấy mã nhân viên mới
        /// </summary>
        /// <returns></returns>
        public async Task<string> GetNewCodeAsync()
        {
            var newCode = await _employeeRepository.GetNewCodeAsync();
            return newCode; 
        }
        /// <summary>
        /// Thêm mới nhân viên - DONE
        /// </summary>
        /// <param name="employee"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<Guid?> CreateAsync(EmployeeCreateDto employeeCreateDto)
        {
            // Validate

            // Create
            var employeeCreate = _mapper.Map<EmployeeCreate>(employeeCreateDto);
            employeeCreate.EmployeeId = new Guid();
            employeeCreate.CreatedDate = DateTime.Now.ToLocalTime();
            employeeCreate.CreatedBy = "Dux";
            employeeCreate.ModifiedDate = DateTime.Now.ToLocalTime();
            employeeCreate.ModifiedBy = "Dux";
            await _employeeRepository.CreateAsync(employeeCreate);

            return employeeCreate.EmployeeId;
        }
        /// <summary>
        /// Cập nhật thông tin nhân viên - DONE
        /// </summary>
        /// <param name="id"></param>
        /// <param name="employeeUpdateDto"></param>
        /// <returns></returns>
        public async Task UpdateAsync(Guid id, EmployeeUpdateDto employeeUpdateDto)
        {
            // Validate
            
            // Update
            var employeeUpdate = _mapper.Map<EmployeeUpdate>(employeeUpdateDto);
            employeeUpdate.ModifiedDate = DateTime.Now.ToLocalTime();
            employeeUpdate.ModifiedBy = "DUX";
            await _employeeRepository.UpdateAsync(id, employeeUpdate);
        }
    }
}
