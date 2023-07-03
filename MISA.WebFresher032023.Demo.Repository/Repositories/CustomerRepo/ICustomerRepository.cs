using MISA.WebFresher032023.Demo.DataLayer.Entities.Input;
using MISA.WebFresher032023.Demo.DataLayer.Entities.Output;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WebFresher032023.Demo.DataLayer.Repositories
{
    public interface ICustomerRepository : IBaseRepository<Customer, CustomerInput>
    {
        /// <summary>
        /// Lấy mã KH mới
        /// </summary>
        /// <returns>Mã nhân viên mới</returns>
        /// /// Author: DNT(20/06/2023)
        Task<string> GetNewCodeAsync();

    }
}
