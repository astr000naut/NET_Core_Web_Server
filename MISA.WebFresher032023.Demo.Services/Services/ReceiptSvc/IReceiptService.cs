

using MISA.WebFresher032023.Demo.BusinessLayer.Dtos.Input;
using MISA.WebFresher032023.Demo.BusinessLayer.Dtos.Output;

namespace MISA.WebFresher032023.Demo.BusinessLayer.Services.ReceiptSvc
{
    public interface IReceiptService : IBaseService<ReceiptDto, ReceiptInputDto>
    {
        Task<string> GetNewReceiptNoAsync();

        Task<long> GetTotalReceive(string keySearch);
    }
}
