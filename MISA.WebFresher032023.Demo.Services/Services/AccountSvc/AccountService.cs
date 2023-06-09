﻿using AutoMapper;
using MISA.WebFresher032023.Demo.BusinessLayer.Dtos.Input;
using MISA.WebFresher032023.Demo.BusinessLayer.Dtos.Output;
using MISA.WebFresher032023.Demo.DataLayer;
using MISA.WebFresher032023.Demo.DataLayer.Entities.Input;
using MISA.WebFresher032023.Demo.DataLayer.Entities.Output;
using MISA.WebFresher032023.Demo.DataLayer.Repositories;
using MISA.WebFresher032023.Demo.DataLayer.Repositories.AccountRepo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WebFresher032023.Demo.BusinessLayer.Services.AccountSvc
{
    public class AccountService : BaseService<Account, AccountDto, AccountInput, AccountInputDto>, IAccountService
    {

        private readonly IUnitOfWork _unitOfWork;
        private readonly IAccountRepository _accountRepository;
        
        public AccountService(IAccountRepository accountRepository, IMapper mapper, IUnitOfWork unitOfWork) : base(accountRepository, mapper, unitOfWork)
        {
            _accountRepository = accountRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<FilteredListDto<AccountDto>> FilterAccountAsync(AccountFilterInputDto accountFilterInputDto)
        {
            Guid uKey = Guid.NewGuid();
            try
            {
                _unitOfWork.setManipulationKey(uKey);
                await _unitOfWork.OpenAsync(uKey);

                var accountFilterInput = _mapper.Map<AccountFilterInput>(accountFilterInputDto);

                // Lấy dữ liệu từ Repository 
                var accountFilteredList = await _accountRepository.FilterAccountAsync(accountFilterInput);

                // Khởi tạo kêt quả trả về
                var filteredListDto = new FilteredListDto<AccountDto>
                {
                    TotalRecord = accountFilteredList.TotalRecord,
                    FilteredList = new List<AccountDto?>()
                };

                // Map dữ liệu nhận được từ Repository sang Dto
                foreach (var account in accountFilteredList.ListData)
                {
                    var accountDto = _mapper.Map<AccountDto>(account);
                    filteredListDto.FilteredList.Add(accountDto);
                }

                return filteredListDto;
            }
            catch
            {
                throw;
            } finally
            {
                await _unitOfWork.CloseAsync(uKey);
            }
            
        }

    }
}
