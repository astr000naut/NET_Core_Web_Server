using MISA.WebFresher032023.Demo.DataLayer.Entities.Input;
using MISA.WebFresher032023.Demo.DataLayer.Entities.Output;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WebFresher032023.Demo.DataLayer.Repositories.AccountRepo
{
    public interface IAccountRepository : IBaseRepository<Account, AccountInput>
    {
        Task<FilteredList<Account>> FilterAccountAsync(AccountFilterInput accountFilterInput);
        Task<bool> ChangeUsingStatusAsync(string mCodeId, bool usingStatus);
    }
}
