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
        /// <summary>
        /// Khởi tạo và lấy kết nối đến DB
        /// </summary>
        /// <returns></returns>
        /// Author: DNT(20/05/2023)
        Task<DbConnection> GetOpenConnectionAsync();


        /// <summary>
        /// Tạo một Entity
        /// </summary>
        /// <param name="tEntityCreate"></param>
        /// <returns></returns>
        /// Author: DNT(20/05/2023)
        Task<bool> CreateAsync(TEntityCreate tEntityCreate);


        /// <summary>
        /// Lấy một Entity theo ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// /// Author: DNT(20/05/2023)
        Task<TEntity?> GetAsync(Guid id);


        /// <summary>
        /// Filter danh sách Entity
        /// </summary>
        /// <param name="skip"></param>
        /// <param name="take"></param>
        /// <param name="keySearch"></param>
        /// <returns></returns>
        /// /// Author: DNT(20/05/2023)
        Task<FilteredList<TEntity>> FilterAsync(int skip, int? take, string keySearch);


        /// <summary>
        /// Cập nhật thông tin của một Entity
        /// </summary>
        /// <param name="id"></param>
        /// <param name="tEntityUpdate"></param>
        /// <returns></returns>
        /// /// Author: DNT(20/05/2023)
        Task<bool> UpdateAsync(Guid id, TEntityUpdate tEntityUpdate);


        /// <summary>
        /// Xóa một Entity theo Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// Author: DNT(20/05/2023)
        Task<bool> DeleteByIdAsync(Guid id);

        /// <summary>
        /// Kiểm tra mã đã tồn tại
        /// </summary>
        /// <param name="id"></param>
        /// <param name="code"></param>
        /// <returns></returns>
        Task<bool> CheckCodeExistAsync(Guid? id, string code);
    }
}
