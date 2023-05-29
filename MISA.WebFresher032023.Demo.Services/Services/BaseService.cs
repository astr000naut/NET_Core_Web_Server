using AutoMapper;
using MISA.WebFresher032023.Demo.BusinessLayer.Dtos.Input;
using MISA.WebFresher032023.Demo.BusinessLayer.Dtos.Output;
using MISA.WebFresher032023.Demo.DataLayer.Entities.Input;
using MISA.WebFresher032023.Demo.DataLayer.Entities.Output;
using MISA.WebFresher032023.Demo.DataLayer.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WebFresher032023.Demo.BusinessLayer.Services
{
    public abstract class BaseService<TEntity, TEntityDto, TEntityCreate, TEntityCreateDto, TEntityUpdate, TEntityUpdateDto>
        : IBaseService<TEntityDto, TEntityCreateDto, TEntityUpdateDto>
    {
        protected readonly IBaseRepository<TEntity, TEntityCreate, TEntityUpdate> _baseRepository;
        protected readonly IMapper _mapper;

        public BaseService(IBaseRepository<TEntity, TEntityCreate, TEntityUpdate> repository, IMapper mapper)
        {
            _baseRepository = repository;
            _mapper = mapper;
        }

        /// <summary>
        /// Tạo một Entity
        /// </summary>
        /// <param name="tEntityCreateDto"></param>
        /// <returns></returns>
        /// Author: DNT(26/05/2023)
        public abstract Task<Guid?> CreateAsync(TEntityCreateDto tEntityCreateDto);

        /// <summary>
        /// Cập nhật thông tin một Entity
        /// </summary>
        /// <param name="id"></param>
        /// <param name="tEntityUpdateDto"></param>
        /// <returns></returns>
        /// Author: DNT(26/05/2023)
        public abstract Task<bool> UpdateAsync(Guid id, TEntityUpdateDto tEntityUpdateDto);

        /// <summary>
        /// Lấy một Entity theo ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// Author: DNT(26/05/2023)
        public async Task<TEntityDto?> GetAsync(Guid id)
        {
            var entity = await _baseRepository.GetAsync(id);
            var entityDto = _mapper.Map<TEntityDto>(entity);
            return entityDto;
        }

        /// <summary>
        /// Filter danh sách Entity
        /// </summary>
        /// <param name="entityFilterDto"></param>
        /// <returns></returns>
        /// Author: DNT(29/05/2023)
        public async Task<FilteredListDto<TEntityDto>> FilterAsync(EntityFilterDto entityFilterDto)
        {
            var entityFilter = _mapper.Map<EntityFilter>(entityFilterDto);
            var tEntityFilteredList = await _baseRepository.FilterAsync(entityFilter);
            var tEntityFilteredListDto = new FilteredListDto<TEntityDto>
            {
                TotalRecord = tEntityFilteredList.TotalRecord,
                FilteredList = new List<TEntityDto?>()
            };
            foreach (var tEntity in tEntityFilteredList.ListData)
            {
                var tEntityDto = _mapper.Map<TEntityDto>(tEntity);
                tEntityFilteredListDto.FilteredList.Add(tEntityDto);
            }
            return tEntityFilteredListDto;
        }

        /// <summary>
        /// Xóa một Entity theo ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// Author: DNT(26/05/2023)
        public async Task<bool> DeleteByIdAsync(Guid id)
        {
            return await _baseRepository.DeleteByIdAsync(id);
        }

        /// <summary>
        /// Kiểm tra trùng mã 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="code"></param>
        /// <returns></returns>
        /// Author: DNT(27/05/2023)
        public async Task<bool> CheckCodeExistAsync(Guid? id, string code)
        {
            return await _baseRepository.CheckCodeExistAsync(id, code);
        }

        /// <summary>
        /// Xóa nhiều Entity
        /// </summary>
        /// <param name="entityIdList"></param>
        /// <returns></returns>
        /// Author: DNT(26/05/2023)
        public async Task<int> DeleteMultipleAsync(List<Guid> entityIdList)
        {
            // Transform list to string
            var stringIdList = string.Join(",", entityIdList);
            return await _baseRepository.DeleteMultipleAsync(stringIdList);
        }
    }
}
