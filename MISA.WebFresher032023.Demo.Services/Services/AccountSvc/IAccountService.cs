using MISA.WebFresher032023.Demo.BusinessLayer.Dtos.Input;
using MISA.WebFresher032023.Demo.BusinessLayer.Dtos.Output;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WebFresher032023.Demo.BusinessLayer.Services.AccountSvc
{
    public interface IAccountService : IBaseService<AccountDto, AccountInputDto>
    {
        Task<FilteredListDto<AccountDto>> FilterAccountAsync(AccountFilterInputDto accountFilterInputDto);

        Task<bool> ChangeUsingStatusAsync(AccountChangeUsingStatusDto accountChangeUsingStatusDto);
    }
}
