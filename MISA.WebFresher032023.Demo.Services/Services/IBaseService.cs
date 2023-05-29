using MISA.WebFresher032023.Demo.BusinessLayer.Dtos.Input;
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
        /// Author: DNT(26/05/2023)
        Task<Guid?> CreateAsync(TEntityCreateDto tEntityCreateDto);

        /// <summary>
        /// Lấy Entity theo ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// Author: DNT(26/05/2023)
        Task<TEntityDto?> GetAsync(Guid id);

        /// <summary>
        /// Filter danh sách đối Entity
        /// </summary>
        /// <param name="entityFilterDto"></param>
        /// <returns></returns>
        /// Author: DNT(29/05/2023)
        Task<FilteredListDto<TEntityDto>> FilterAsync(EntityFilterDto entityFilterDto);

        /// <summary>
        /// Cập nhật một Entity
        /// </summary>
        /// <param name="id"></param>
        /// <param name="tEntityUpdateDto"></param>
        /// <returns></returns>
        /// Author: DNT(26/05/2023)
        Task<bool> UpdateAsync(Guid id, TEntityUpdateDto tEntityUpdateDto);

        /// <summary>
        /// Xóa một Entity theo ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// Author: DNT(26/05/2023)
        Task<bool> DeleteByIdAsync(Guid id);

        /// <summary>
        /// Kiểm tra mã của Entity đã tồn tại
        /// </summary>
        /// <param name="id"></param>
        /// <param name="code"></param>
        /// <returns></returns>
        /// Author: DNT(26/05/2023)
        Task<bool> CheckCodeExistAsync(Guid? id, string code);

        /// <summary>
        /// Xóa nhiều Entity theo danh sách ID
        /// </summary>
        /// <param name="entityIdList"></param>
        /// <returns></returns>
        /// Author: DNT(26/05/2023)

        Task<int> DeleteMultipleAsync(List<Guid> entityIdList);
    }
}
