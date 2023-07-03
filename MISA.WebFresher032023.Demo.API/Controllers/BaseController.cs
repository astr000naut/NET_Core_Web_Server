using Microsoft.AspNetCore.Mvc;
using MISA.WebFresher032023.Demo.BusinessLayer.Dtos.Input;
using MISA.WebFresher032023.Demo.BusinessLayer.Dtos.Output;
using MISA.WebFresher032023.Demo.BusinessLayer.Services;
using MISA.WebFresher032023.Demo.Common.Enums;
using MISA.WebFresher032023.Demo.Common.Exceptions;

namespace MISA.WebFresher032023.Demo.API.Controllers
{

    [ApiController]
    public abstract class BaseController<TEntityDto, TEntityInputDto> : ControllerBase
    {
        protected readonly IBaseService<TEntityDto, TEntityInputDto> _baseService;

        public BaseController(IBaseService<TEntityDto, TEntityInputDto> baseService)
        {
            _baseService = baseService;
        }

        /// <summary>
        /// API Tạo mới một đối tượng
        /// </summary>
        /// <param name="tEntityInputDto"></param>
        /// <returns>Id của đối tượng được tạo</returns>
        /// Author: DNT(24/05/2023)
        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody] TEntityInputDto tEntityInputDto)
        {
            var newId =  await _baseService.CreateAsync(tEntityInputDto);
            return Created("", newId);
        }

        /// <summary>
        /// Lấy một đối tượng theo ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Thông tin của đối tượng</returns>
        /// Author: DNT(24/05/2023)
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAsync(Guid id)
        {
            var entity = await _baseService.GetAsync(id);
            return entity == null
                ? throw new NotFoundException(Error.NotFound, Error.NotFoundEmployeeMsg, Error.NotFoundEmployeeMsg)
                : (IActionResult)Ok(entity);
        }

        /// <summary>
        /// Filter danh sách đối tượng
        /// </summary>
        /// <param name="entityFilterDto"></param>
        /// <returns>filtered List</returns>
        /// Author: DNT(29/05/2023)
        [Route("Filter")]
        [HttpGet]
        public async Task<IActionResult> FilterAsync([FromQuery] EntityFilterDto entityFilterDto)
        {
            var filteredList =  await _baseService.FilterAsync(entityFilterDto);
            return Ok(filteredList);
        }

        /// <summary>
        /// Cập nhật một đối tượng
        /// </summary>
        /// <param name="id">Id của đối tượng</param>
        /// <param name="tEntityInputDto">EntityUpdateDto của đối tượng</param>
        /// <returns>Giá trị boolean đã cập nhật hay chưa</returns>
        /// Author: DNT(24/05/2023)
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAsync(Guid id, [FromBody] TEntityInputDto tEntityInputDto)
        {
           var isUpdated = await _baseService.UpdateAsync(id, tEntityInputDto);
            return Ok(isUpdated);
        }

        /// <summary>
        /// Xóa một đối tượng theo ID
        /// </summary>
        /// <param name="id">Id của đối tượng</param>
        /// <returns>Giá trị boolean biểu thị đã xóa hay chưa</returns>
        /// Author: DNT(24/05/2023)
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(Guid id)
        {
            var isDeleted = await _baseService.DeleteByIdAsync(id);
            return Ok(isDeleted);
        }


        /// <summary>
        /// Xóa nhiều đối tượng theo danh sách ID
        /// </summary>
        /// <param name="entityIdList">Mảng Id các đối tượng</param>
        /// <returns>int : số lượng đối tượng đã xóa thành công</returns>
        /// <exception cref="BadInputException"></exception>
        /// Author: DNT(24/05/2023)
        [Route("DeleteMultiple")]
        [HttpPost]
        public async Task<IActionResult> DeleteMultipleAsync([FromBody] List<Guid> entityIdList)
        {
            // Nếu danh sách đối tượng quá lớn thì trả về Exception
            if (entityIdList.Count > 50)
            {
                throw new BadInputException(Error.BadInput, Error.IdListOversizeMsg, Error.IdListOversizeMsg);
            }
            var deletedAmount = await _baseService.DeleteMultipleAsync(entityIdList);
            return Ok(deletedAmount);
        }

        /// <summary>
        /// Kiểm tra mã đã tồn tại
        /// </summary>
        /// <param name="id">Id của đối tượng (chỉ cần trong trường hợp cập nhật)</param>
        /// <param name="code">Mã</param>
        /// <returns>Giá trị boolean biểu thị mã đã tồn tại hay chưa</returns>
        /// Author: DNT(24/05/2023)
        [Route("CheckCodeExist")]
        [HttpGet]
        public async Task<IActionResult> CheckCodeExistAsync(Guid? id, string code)
        {
            var isExist = await _baseService.CheckCodeExistAsync(id, code);
            return Ok(isExist);
        }
    }
}
