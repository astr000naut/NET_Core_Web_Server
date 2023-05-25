using MISA.WebFresher032023.Demo.BusinessLayer.Dtos.Output;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WebFresher032023.Demo.BusinessLayer.Services
{
    public interface IBaseService<TEntityDto, TEntityCreateDto, TEntityUpdateDto>
    {

        /// <summary>
        /// Tạo mới Entity
        /// </summary>
        /// <param name="tEntityCreateDto"></param>
        /// <returns></returns>
        Task<Guid?> CreateAsync(TEntityCreateDto tEntityCreateDto);

        /// <summary>
        /// Lấy Entity theo ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<TEntityDto?> GetAsync(Guid id);

        /// <summary>
        /// Filter danh sách Entity
        /// </summary>
        /// <param name="skip"></param>
        /// <param name="take"></param>
        /// <param name="keySearch"></param>
        /// <returns></returns>
        Task<FilteredListDto<TEntityDto>> FilterAsync(int skip, int? take, string keySearch);

        /// <summary>
        /// Cập nhật một Entity
        /// </summary>
        /// <param name="id"></param>
        /// <param name="tEntityUpdateDto"></param>
        /// <returns></returns>
        Task<bool> UpdateAsync(Guid id, TEntityUpdateDto tEntityUpdateDto);

        /// <summary>
        /// Xóa một Entity theo ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<bool> DeleteByIdAsync(Guid id);

        /// <summary>
        /// Kiểm tra mã của Entity đã tồn tại
        /// </summary>
        /// <param name="id"></param>
        /// <param name="code"></param>
        /// <returns></returns>
        Task<bool> CheckCodeExistAsync(Guid? id, string code);

        /// <summary>
        /// Xóa nhiều Entity theo danh sách ID
        /// </summary>
        /// <param name="entityIdList"></param>
        /// <returns></returns>

        Task<int> DeleteMultipleAsync(List<Guid> entityIdList);
    }
}
