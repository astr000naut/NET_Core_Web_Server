using AutoMapper;
using MISA.WebFresher032023.Demo.BusinessLayer.Dtos.Input;
using MISA.WebFresher032023.Demo.BusinessLayer.Dtos.Output;
using MISA.WebFresher032023.Demo.Common.Enums;
using MISA.WebFresher032023.Demo.Common.Exceptions;
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
        /// Tạo mới một nhân viên
        /// </summary>
        /// <param name="employeeCreateDto"></param>
        /// <returns></returns>
        /// <exception cref="ConflictException"></exception>
        public override async Task<Guid?> CreateAsync(EmployeeCreateDto employeeCreateDto)
        {
            // Kiểm tra đơn vị có tồn tại
            var isDepartmentIdValid = await _employeeRepository.ValidateDepartmentId(employeeCreateDto.DepartmentId);
            if (!isDepartmentIdValid)
            {
                throw new ConflictException(Error.ConflictCode, Error.InvalidDepartmentId, Error.InvalidDepartmentId);
            }
            // Kiểm tra mã đã tồn tại
            var isEmployeeCodeExist = await _baseRepository.CheckCodeExistAsync(null, employeeCreateDto.EmployeeCode);
            if (isEmployeeCodeExist)
            {
                throw new ConflictException(Error.ConflictCode, Error.EmployeeCodeHasExist, Error.EmployeeCodeHasExist);
            }

            // Tạo mới nhân viên 
             var employeeCreate = _mapper.Map<EmployeeCreate>(employeeCreateDto);
             employeeCreate.EmployeeId = Guid.NewGuid();
             employeeCreate.CreatedDate = DateTime.Now.ToLocalTime();
             employeeCreate.CreatedBy = "Dux";
             var isCreated = await _employeeRepository.CreateAsync(employeeCreate);

             return isCreated ? employeeCreate.EmployeeId : null;
        }
       
        /// <summary>
        /// Cập nhật thông tin nhân viên
        /// </summary>
        /// <param name="id"></param>
        /// <param name="employeeUpdateDto"></param>
        /// <returns></returns>
        /// <exception cref="ConflictException"></exception>
        public override async Task<bool> UpdateAsync(Guid id, EmployeeUpdateDto employeeUpdateDto)
        {
            _ = await _employeeRepository.GetAsync(id) ?? throw new ConflictException(Error.ConflictCode, Error.InvalidEmployeeId, Error.InvalidEmployeeId);

            // Kiểm tra đơn vị có tồn tại
            var isDepartmentIdValid = await _employeeRepository.ValidateDepartmentId(employeeUpdateDto.DepartmentId);
            if (!isDepartmentIdValid)
            {
                throw new ConflictException(Error.ConflictCode, Error.InvalidDepartmentId, Error.InvalidDepartmentId);
            }
            // Kiểm tra mã đã tồn tại
            var isEmployeeCodeExist = await _baseRepository.CheckCodeExistAsync(id, employeeUpdateDto.EmployeeCode);
            if (isEmployeeCodeExist)
            {
                throw new ConflictException(Error.ConflictCode, Error.EmployeeCodeHasExist, Error.EmployeeCodeHasExist);
            }

            // Cập nhật thông tin nhân viên 
            var employeeUpdate = _mapper.Map<EmployeeUpdate>(employeeUpdateDto);
            employeeUpdate.EmployeeId = id;
            employeeUpdate.ModifiedDate = DateTime.Now.ToLocalTime();
            employeeUpdate.ModifiedBy = "Dux";
            return await _employeeRepository.UpdateAsync(id, employeeUpdate);
        }
    }
}
