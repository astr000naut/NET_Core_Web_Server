﻿using AutoMapper;
using MISA.WebFresher032023.Demo.BusinessLayer.Dtos.Input;
using MISA.WebFresher032023.Demo.BusinessLayer.Dtos.Output;
using MISA.WebFresher032023.Demo.Common.Enums;
using MISA.WebFresher032023.Demo.Common.Exceptions;
using MISA.WebFresher032023.Demo.Common.Resources;
using MISA.WebFresher032023.Demo.DataLayer;
using MISA.WebFresher032023.Demo.DataLayer.Entities.Input;
using MISA.WebFresher032023.Demo.DataLayer.Entities.Output;
using MISA.WebFresher032023.Demo.DataLayer.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Dapper.SqlMapper;

namespace MISA.WebFresher032023.Demo.BusinessLayer.Services
{
    public class CustomerService : BaseService<Customer, CustomerDto, CustomerInput, CustomerInputDto>, ICustomerService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICustomerRepository _customerRepository;

        public CustomerService(ICustomerRepository customerRepository, IMapper mapper, IUnitOfWork unitOfWork) : base(customerRepository, mapper, unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _customerRepository = customerRepository;
        }

        /// <summary>
        /// Lấy mã KH mới
        /// </summary>
        /// <returns>Mã KH mới</returns>
        /// Author: DNT(26/06/2023)
        public async Task<string> GetNewCodeAsync()
        {
            Guid uKey = Guid.NewGuid();
            try
            {
                _unitOfWork.setManipulationKey(uKey);
                await _unitOfWork.OpenAsync(uKey);
                var newCode = await _customerRepository.GetNewCodeAsync();
                return newCode;
            } catch
            {
                throw;
            } finally
            {
                await _unitOfWork.CloseAsync(uKey);
            }
            
        }

        public override async Task<bool> UpdateAsync(Guid id, CustomerInputDto customerInputDto)
        {
            Guid uKey = Guid.NewGuid();
           try
            {
                _unitOfWork.setManipulationKey(uKey);
                await _unitOfWork.OpenAsync(uKey);
                await _unitOfWork.BeginAsync(uKey);

                // Kiểm tra khách hàng có tồn tại
                _ = await _customerRepository.GetAsync(id) ?? throw new ConflictException(Error.ConflictCode, Error.InvalidCustomerIdMsg, Error.InvalidCustomerIdMsg);

                // Kiểm tra mã đã tồn tại
                var isCustomerCodeExist = await _baseRepository.CheckCodeExistAsync(id, customerInputDto.CustomerCode);
                if (isCustomerCodeExist)
                {
                    throw new ConflictException(Error.ConflictCode, Error.EmployeeCodeHasExistMsg, Error.EmployeeCodeHasExistMsg);
                }

                var customerInput = _mapper.Map<CustomerInput>(customerInputDto);
                customerInput.CustomerId = id;
                customerInput.ModifiedDate = DateTime.Now.ToLocalTime();
                customerInput.ModifiedBy = Value.ModifiedBy;

                var result = await _customerRepository.UpdateAsync(customerInput);
                await _unitOfWork.CommitAsync(uKey);
                return result;

            } catch
            {
                throw;
            }
            finally
            {
                await _unitOfWork.DisposeAsync(uKey);
                await _unitOfWork.CloseAsync(uKey);
            }
        }

    }
}
