﻿using AutoMapper;
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
    public class DepartmentService : BaseService<Department, DepartmentDto, DepartmentInput, DepartmentInputDto>, IDepartmentService
    {
        private readonly IDepartmentRepository _departmentRepository;

        public DepartmentService(IDepartmentRepository departmentRepository, IMapper mapper) : base(departmentRepository, mapper)
        {
            _departmentRepository = departmentRepository;
        }

        /// <summary>
        /// Tạo mới một đơn vị
        /// </summary>
        /// <param name="departmentInputDto"></param>
        /// <returns></returns>
        /// Author: DNT(25/05/2023)
        /// Modified: DNT(09/06/2023)
        public override async Task<Guid?> CreateAsync(DepartmentInputDto departmentInputDto)
        {
            return await base.CreateAsync(departmentInputDto);
        }

        /// <summary>
        /// Cập nhật thông tin Department - DONE
        /// </summary>
        /// <param name="departmentId"></param>
        /// <param name="departmentInputDto"></param>
        /// <returns></returns>
        /// Author: DNT(25/05/2023)
        /// Modified: DNT(09/06/2023)
        public override async Task<bool> UpdateAsync(Guid departmentId, DepartmentInputDto departmentInputDto)
        {
            // Kiểm tra mã đã tồn tại
            var isDepartmentCodeExist = await _departmentRepository.CheckCodeExistAsync(departmentId, departmentInputDto.DepartmentCode);

            if (isDepartmentCodeExist)
            {
                throw new ConflictException(Error.ConflictCode, Error.DepartmentCodeHasExistMsg, Error.DepartmentCodeHasExistMsg);
            }

            return await base.UpdateAsync(departmentId, departmentInputDto);
        }

    }
}
