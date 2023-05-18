using AutoMapper;
using MISA.WebFresher032023.Demo.BusinessLayer.DepartmentService.Dto.FromClient;
using MISA.WebFresher032023.Demo.BusinessLayer.DepartmentService.Dto.Output;
using MISA.WebFresher032023.Demo.DataLayer.Entities.Input;
using MISA.WebFresher032023.Demo.DataLayer.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WebFresher032023.Demo.BusinessLayer.DepartmentService
{
    public class DepartmentService : IDepartmentService
    {
        private readonly IDepartmentRepository _departmentRepository;
        private readonly IMapper _mapper;

        public DepartmentService(IDepartmentRepository departmentRepository, IMapper mapper)
        {
            _departmentRepository = departmentRepository;
            _mapper = mapper;
        }
        /// <summary>
        /// Xóa một Department theo ID - DONE
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task DeleteByIdAsync(Guid id)
        {
            await _departmentRepository.DeleteByIdAsync(id);
        }

        /// <summary>
        /// Lấy một Department theo ID - DONE
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<DepartmentDto?> GetAsync(Guid id)
        {
            var department = await _departmentRepository.GetAsync(id);
            var departmentDto = _mapper.Map<DepartmentDto>(department);
            return departmentDto;
        }

        /// <summary>
        /// Lấy tất cả Department - DONE
        /// </summary>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<IEnumerable<DepartmentDto?>> GetAllAsync()
        {
            var allDepartment = await _departmentRepository.GetAllAsync();
            var allDepartmentDto = new List<DepartmentDto>();
            foreach (var department in allDepartment)
            {
                allDepartmentDto.Add(_mapper.Map<DepartmentDto>(department));
            }
            return allDepartmentDto;
        }

        /// <summary>
        /// Tạo mới Department - DONE
        /// </summary>
        /// <param name="departmentCreateDto"></param>
        /// <returns></returns>
        public async Task CreateAsync(DepartmentCreateDto departmentCreateDto)
        {
            var departmentCreate = _mapper.Map<DepartmentCreate>(departmentCreateDto);
            departmentCreate.DepartmentId = new Guid();
            departmentCreate.DepartmentCode = "";
            departmentCreate.CreatedDate = DateTime.Now.ToLocalTime();
            departmentCreate.CreatedBy = "Dux";
            departmentCreate.ModifiedDate = DateTime.Now.ToLocalTime();
            departmentCreate.ModifiedBy = "Dux";
            await _departmentRepository.CreateAsync(departmentCreate);
        }

        /// <summary>
        /// Cập nhật thông tin Department - DONE
        /// </summary>
        /// <param name="departmentId"></param>
        /// <param name="departmentUpdateDto"></param>
        /// <returns></returns>
        public async Task UpdateAsync(Guid departmentId, DepartmentUpdateDto departmentUpdateDto)
        {
            var departmentUpdate = _mapper.Map<DepartmentUpdate>(departmentUpdateDto);
            departmentUpdate.ModifiedDate = DateTime.Now.ToLocalTime();
            departmentUpdate.ModifiedBy = "Dux";
            await _departmentRepository.UpdateAsync(departmentId, departmentUpdate);
        }
    }
}
