﻿using AutoMapper;
using MISA.WebFresher032023.Demo.BusinessLayer.Dtos.Input;
using MISA.WebFresher032023.Demo.BusinessLayer.Dtos.Output;
using MISA.WebFresher032023.Demo.Common.Enums;
using MISA.WebFresher032023.Demo.Common.Exceptions;
using MISA.WebFresher032023.Demo.Common.Resources;
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
        private readonly ICustomerRepository _customerRepository;

        public CustomerService(ICustomerRepository customerRepository, IMapper mapper) : base(customerRepository, mapper)
        {
            _customerRepository = customerRepository;
        }

        /// <summary>
        /// Lấy mã KH mới
        /// </summary>
        /// <returns>Mã KH mới</returns>
        /// Author: DNT(26/06/2023)
        public async Task<string> GetNewCodeAsync()
        {
            var newCode = await _customerRepository.GetNewCodeAsync();
            return newCode;
        }

        public override async Task<bool> UpdateAsync(Guid id, CustomerInputDto customerInputDto)
        {
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

            return await _customerRepository.UpdateAsync(customerInput);
        }

    }
}
