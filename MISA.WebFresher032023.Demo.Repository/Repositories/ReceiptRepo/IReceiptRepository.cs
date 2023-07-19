using MISA.WebFresher032023.Demo.DataLayer.Entities.Input;
using MISA.WebFresher032023.Demo.DataLayer.Entities.Output;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WebFresher032023.Demo.DataLayer.Repositories.ReceiptRepo
{
    public interface IReceiptRepository : IBaseRepository<Receipt, ReceiptInput>
    {
        Task<bool> InsertReceiptDetailAsync(ReceiptDetailInput receiptDetailInput);
        Task<bool> UpdateReceiptDetailAsync(ReceiptDetailInput receiptDetailInput);
        Task<bool> DeleteReceiptDetailAsync(Guid? id);
        Task<IEnumerable<ReceiptDetail>> GetReceiptDetailListAsync(Guid id);

        Task<string> GetNewReceiptNoAsync();

        Task<long> GetTotalReceive(string keySearch);
    }
}
