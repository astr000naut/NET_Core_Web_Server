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
    public interface IUnitOfWork : IDisposable
    {
        DbConnection Connection { get; }
        DbTransaction Transaction { get; }
        void SetManipulationKey(int key);
        int GetManipulationKey();
        Task OpenAsync(int key);
        Task CloseAsync(int key);
        void Begin(int key);
        Task BeginAsync(int key);
        void Commit(int key);
        Task CommitAsync(int key);
        void Rollback(int key);
        Task RollbackAsync(int key);
        Task DisposeAsync(int key);
    }
}
