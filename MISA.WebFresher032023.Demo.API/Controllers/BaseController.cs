using Microsoft.AspNetCore.Mvc;
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

        [HttpPost]
        public async Task<Guid?> PostAsync([FromBody] TEntityCreateDto tEntityCreateDto)
        {
            return await _baseService.CreateAsync(tEntityCreateDto);
        }

        [HttpGet("{id}")]
        public async Task<TEntityDto?> GetAsync(Guid id)
        {
            return await _baseService.GetAsync(id);
        }

        [Route("Filter")]
        [HttpGet]
        public async Task<FilteredListDto<TEntityDto>> FilterAsync(int skip, int? take, string? keySearch)
        {
            return await _baseService.FilterAsync(skip, take, keySearch ?? "");
        }

        [HttpPut("{id}")]
        public async Task<bool> PutAsync(Guid id, [FromBody] TEntityUpdateDto tEntityUpdateDto)
        {
            return await _baseService.UpdateAsync(id, tEntityUpdateDto);
        }

        [HttpDelete("{id}")]
        public async Task<bool> DeleteAsync(Guid id)
        {
            return await _baseService.DeleteByIdAsync(id);
        }

        [Route("CheckCodeExist")]
        [HttpGet]
        public async Task<bool> CheckEmployeeCodeExistAsync(Guid? id, string code)
        {
            return await _baseService.CheckCodeExistAsync(id, code);
        }

    }
}
