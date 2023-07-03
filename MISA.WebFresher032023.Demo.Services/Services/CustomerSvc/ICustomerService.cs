using MISA.WebFresher032023.Demo.BusinessLayer.Dtos.Input;
using MISA.WebFresher032023.Demo.BusinessLayer.Dtos.Output;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WebFresher032023.Demo.BusinessLayer.Services
{
    public interface ICustomerService : IBaseService<CustomerDto, CustomerInputDto>
    {
        /// <summary>
        /// Sinh mã KH mới
        /// </summary>
        /// <returns>Mã KH mới</returns>
        Task<string> GetNewCodeAsync();
    }
}
