using MISA.WebFresher032023.Demo.DataLayer.Repositories;
using MISA.WebFresher032023.Demo.DataLayer.Repositories.AccountRepo;
using MISA.WebFresher032023.Demo.DataLayer.Repositories.GroupRepo;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace MISA.WebFresher032023.Demo.DataLayer
{
    public interface IUnitOfWork:IDisposable
    {
        DbConnection Connection { get; }
        DbTransaction Transaction { get; }
        void setManipulationKey(Guid key);
        Task OpenAsync(Guid key);
        Task CloseAsync(Guid key);
        void Begin(Guid key);
        Task BeginAsync(Guid key);
        void Commit(Guid key);
        Task CommitAsync(Guid key);
        void Rollback(Guid key);
        Task RollbackAsync(Guid key);
        Task DisposeAsync(Guid key);
    }
}
