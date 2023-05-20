using MISA.WebFresher032023.Demo.BusinessLayer.Dtos.Output;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WebFresher032023.Demo.BusinessLayer
{
    public interface IBaseService<TEntityDto, TEntityCreateDto, TEntityUpdateDto>
    {
        
        Task<Guid?> CreateAsync(TEntityCreateDto tEntityCreateDto);
        Task<TEntityDto?> GetAsync(Guid id);
        Task<FilteredListDto<TEntityDto>> FilterAsync(int skip, int take, string keySearch);
        Task UpdateAsync(Guid id, TEntityUpdateDto tEntityUpdateDto);
        Task DeleteByIdAsync(Guid id);
    }
}
