using MISA.WebFresher032023.Demo.BusinessLayer.Dtos.Input;
using MISA.WebFresher032023.Demo.BusinessLayer.Dtos.Output;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WebFresher032023.Demo.BusinessLayer.Services
{
    public interface IBaseService<TEntityDto, TEntityInputDto>
    {

        /// <summary>
        /// Tạo mới Entity
        /// </summary>
        /// <param name="tEntityInputDto"></param>
        /// <returns>ID của entity mới tạo</returns>
        /// Author: DNT(26/05/2023)
        Task<Guid?> CreateAsync(TEntityInputDto tEntityInputDto);

        /// <summary>
        /// Lấy Entity theo ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns>EntityDto</returns>
        /// Author: DNT(26/05/2023)
        Task<TEntityDto?> GetAsync(Guid id);

        /// <summary>
        /// Filter danh sách Entity
        /// </summary>
        /// <param name="entityFilterDto"></param>
        /// <returns>FilteredListDto chứa danh sách các EntityDto tìm được</returns>
        /// Author: DNT(29/05/2023)
        Task<FilteredListDto<TEntityDto>> FilterAsync(EntityFilterDto entityFilterDto);

        /// <summary>
        /// Cập nhật một Entity
        /// </summary>
        /// <param name="id"></param>
        /// <param name="tEntityInputDto"></param>
        /// <returns>Giá trị boolean biểu thị việc cập nhật thành công hay không</returns>
        /// Author: DNT(26/05/2023)
        Task<bool> UpdateAsync(Guid id, TEntityInputDto tEntityInputDto);

        /// <summary>
        /// Xóa một Entity theo ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Giá trị boolean biểu thị việc xóa có thành công hay không</returns>
        /// Author: DNT(26/05/2023)
        Task<bool> DeleteByIdAsync(Guid id);

        /// <summary>
        /// Kiểm tra mã của Entity đã tồn tại
        /// </summary>
        /// <param name="id">ID của entity (trong trường hợp cập nhật entity)</param>
        /// <param name="code">Mã</param>
        /// <returns>Giá trị boolean biểu thị mã đã tồn tại hay chưa</returns>
        /// Author: DNT(26/05/2023)
        Task<bool> CheckCodeExistAsync(Guid? id, string code);

        /// <summary>
        /// Xóa nhiều Entity theo danh sách ID
        /// </summary>
        /// <param name="entityIdList"></param>
        /// <returns>Số lượng entity đã xóa thành công</returns>
        /// Author: DNT(26/05/2023)

        Task<int> DeleteMultipleAsync(List<Guid> entityIdList);

        Task<byte[]> ExportExcelAsync(ExportExcelDto exportExcelDto);

    }
}
