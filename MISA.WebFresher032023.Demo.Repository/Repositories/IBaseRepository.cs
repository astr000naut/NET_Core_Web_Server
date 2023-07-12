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
    public interface IBaseRepository<TEntity, TEntityInput>
    {
        /// <summary>
        /// Khởi tạo và lấy kết nối đến DB
        /// </summary>
        /// <returns></returns>
        /// Author: DNT(20/05/2023)
/*        Task<DbConnection> GetOpenConnectionAsync();*/


        /// <summary>
        /// Tạo một Entity
        /// </summary>
        /// <param name="tEntityInput"></param>
        /// <returns>Giá trị boolean biểu thị việc tạo entity thành công hay không</returns>
        /// Author: DNT(20/05/2023)
        Task<bool> CreateAsync(TEntityInput tEntityInput);


        /// <summary>
        /// Lấy một Entity theo ID
        /// </summary>
        /// <param name="id">ID của engity</param>
        /// <returns>Entity tìm thấy nếu có</returns>
        /// Author: DNT(20/05/2023)
        Task<TEntity?> GetAsync(Guid id);


        /// <summary>
        /// Filter danh sách Entity
        /// </summary>
        /// <param name="entityFilter"></param>
        /// <returns>Filtered List chứa danh sách các entity tìm được</returns>
        /// Author: DNT(29/05/2023)
        Task<FilteredList<TEntity>> FilterAsync(EntityFilter entityFilter);


        /// <summary>
        /// Cập nhật thông tin của một Entity
        /// </summary>
        /// <param name="tEntityInput"></param>
        /// <returns>Giá trị boolean biểu thị cập nhật thành công hay không</returns>
        /// /// Author: DNT(20/05/2023)
        Task<bool> UpdateAsync(TEntityInput tEntityInput);


        /// <summary>
        /// Xóa một Entity theo Id
        /// </summary>
        /// <param name="id">ID của entity</param>
        /// <returns>Giá trị boolean biểu thị xóa thành công hay không</returns>
        /// Author: DNT(20/05/2023)
        Task<bool> DeleteByIdAsync(Guid id);

        /// <summary>
        /// Kiểm tra mã đã tồn tại
        /// </summary>
        /// <param name="id">ID của entity (chỉ cần trong trường hợp cập nhật)</param>
        /// <param name="code">Mã</param>
        /// <returns>Giá trị boolean thể hiện mã có tồn tại hay không</returns>
        /// Author: DNT(20/05/2023)
        Task<bool> CheckCodeExistAsync(Guid? id, string code);

        /// <summary>
        /// Xóa hàng loạt
        /// </summary>
        /// <param name="stringIdList">Các ID của entity cần xóa nối với nhau, ngăn cách bởi dấu phẩy</param>
        /// <returns>Số lượng entity đã bị xóa</returns>
        /// Author: DNT(20/05/2023)
        Task<int> DeleteMultipleAsync(string stringIdList);
    }
}
