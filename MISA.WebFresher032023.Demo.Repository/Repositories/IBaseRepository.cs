using MISA.WebFresher032023.Demo.DataLayer.Entities.Input;
using MISA.WebFresher032023.Demo.DataLayer.Entities.Output;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WebFresher032023.Demo.DataLayer.Repositories
{
    public interface IBaseRepository<TEntity, TEntityCreate, TEntityUpdate>
    {
        Task<DbConnection> GetOpenConnectionAsync();

        Task CreateAsync(TEntityCreate tEntityCreate);
        Task<TEntity?> GetAsync(Guid id);
        Task<FilteredList<TEntity>> FilterAsync(int skip, int take, string keySearch);
        Task UpdateAsync(Guid id, TEntityUpdate tEntityUpdate);
        Task DeleteByIdAsync(Guid id);
    }
}
