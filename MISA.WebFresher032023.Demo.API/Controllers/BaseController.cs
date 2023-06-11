﻿using Microsoft.AspNetCore.Mvc;
using MISA.WebFresher032023.Demo.BusinessLayer.Dtos.Input;
using MISA.WebFresher032023.Demo.BusinessLayer.Dtos.Output;
using MISA.WebFresher032023.Demo.BusinessLayer.Services;
using MISA.WebFresher032023.Demo.Common.Enums;
using MISA.WebFresher032023.Demo.Common.Exceptions;

namespace MISA.WebFresher032023.Demo.API.Controllers
{

    [ApiController]
    public abstract class BaseController<TEntityDto, TEntityCreateDto, TEntityUpdateDto> : ControllerBase
    {
        protected readonly IBaseService<TEntityDto, TEntityCreateDto, TEntityUpdateDto> _baseService;

        public BaseController(IBaseService<TEntityDto, TEntityCreateDto, TEntityUpdateDto> baseService)
        {
            _baseService = baseService;
        }

        /// <summary>
        /// Tạo mới một đối tượng
        /// </summary>
        /// <param name="tEntityCreateDto"></param>
        /// <returns></returns>
        /// Author: DNT(24/05/2023)
        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody] TEntityCreateDto tEntityCreateDto)
        {
            var newId =  await _baseService.CreateAsync(tEntityCreateDto);
            return Created("", newId);
        }

        /// <summary>
        /// Lấy một đối tượng theo ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
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
        /// <returns></returns>
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
        /// <param name="id"></param>
        /// <param name="tEntityUpdateDto"></param>
        /// <returns></returns>
        /// Author: DNT(24/05/2023)
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAsync(Guid id, [FromBody] TEntityUpdateDto tEntityUpdateDto)
        {
           var isUpdated = await _baseService.UpdateAsync(id, tEntityUpdateDto);
            return Ok(isUpdated);
        }

        /// <summary>
        /// Xóa một đối tượng theo ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
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
        /// <param name="entityIdList"></param>
        /// <returns></returns>
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
        /// <param name="id"></param>
        /// <param name="code"></param>
        /// <returns></returns>
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
