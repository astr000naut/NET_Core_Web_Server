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
        [HttpPost]
        public async Task<Guid?> PostAsync([FromBody] TEntityCreateDto tEntityCreateDto)
        {
            return await _baseService.CreateAsync(tEntityCreateDto);
        }

        /// <summary>
        /// Lấy một đối tượng theo ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<TEntityDto?> GetAsync(Guid id)
        {
            return await _baseService.GetAsync(id);
        }

        /// <summary>
        /// Filter danh sách đối tượng
        /// </summary>
        /// <param name="skip"></param>
        /// <param name="take"></param>
        /// <param name="keySearch"></param>
        /// <returns></returns>
        [Route("Filter")]
        [HttpGet]
        public async Task<FilteredListDto<TEntityDto>> FilterAsync(int skip, int? take, string? keySearch)
        {
            return await _baseService.FilterAsync(skip, take, keySearch ?? "");
        }

        /// <summary>
        /// Cập nhật một đối tượng
        /// </summary>
        /// <param name="id"></param>
        /// <param name="tEntityUpdateDto"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public async Task<bool> PutAsync(Guid id, [FromBody] TEntityUpdateDto tEntityUpdateDto)
        {
            return await _baseService.UpdateAsync(id, tEntityUpdateDto);
        }

        /// <summary>
        /// Xóa một đối tượng theo ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<bool> DeleteAsync(Guid id)
        {
            return await _baseService.DeleteByIdAsync(id);
        }


        /// <summary>
        /// Xóa nhiều đối tượng theo danh sách ID
        /// </summary>
        /// <param name="entityIdList"></param>
        /// <returns></returns>
        /// <exception cref="BadInputException"></exception>
        [Route("DeleteMultiple")]
        [HttpPost]
        public async Task<int> DeleteMultipleAsync([FromBody] List<Guid> entityIdList)
        {
            // Nếu danh sách đối tượng quá lớn thì trả về Exception
            if (entityIdList.Count > 50)
            {
                throw new BadInputException(Error.BadInput, Error.IdListOversizeMsg, Error.IdListOversizeMsg);
            }
            return await _baseService.DeleteMultipleAsync(entityIdList);
        }

        /// <summary>
        /// Kiểm tra mã đã tồn tại
        /// </summary>
        /// <param name="id"></param>
        /// <param name="code"></param>
        /// <returns></returns>
        [Route("CheckCodeExist")]
        [HttpGet]
        public async Task<bool> CheckCodeExistAsync(Guid? id, string code)
        {
            return await _baseService.CheckCodeExistAsync(id, code);
        }

    }
}