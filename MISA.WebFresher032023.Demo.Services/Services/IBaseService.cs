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

        Task<Guid?> CreateAsync(TEntityCreateDto tEntityCreateDto);
        Task<TEntityDto?> GetAsync(Guid id);
        Task<FilteredListDto<TEntityDto>> FilterAsync(int skip, int? take, string keySearch);
        Task<bool> UpdateAsync(Guid id, TEntityUpdateDto tEntityUpdateDto);
        Task<bool> DeleteByIdAsync(Guid id);
        Task<bool> CheckCodeExistAsync(Guid? id, string code);
    }
}
