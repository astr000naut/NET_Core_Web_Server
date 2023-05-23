using AutoMapper;
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

        public abstract Task<Guid?> CreateAsync(TEntityCreateDto tEntityCreateDto);
        public abstract Task<bool> UpdateAsync(Guid id, TEntityUpdateDto tEntityUpdateDto);

        public async Task<TEntityDto?> GetAsync(Guid id)
        {
            var entity = await _baseRepository.GetAsync(id);
            var entityDto = _mapper.Map<TEntityDto>(entity);
            return entityDto;
        }

        public async Task<FilteredListDto<TEntityDto>> FilterAsync(int skip, int? take, string keySearch)
        {
            var tEntityFilteredList = await _baseRepository.FilterAsync(skip, take, keySearch);
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

        public async Task<bool> DeleteByIdAsync(Guid id)
        {
            return await _baseRepository.DeleteByIdAsync(id);
        }

        public async Task<bool> CheckCodeExistAsync(Guid? id, string code)
        {
            return await _baseRepository.CheckCodeExistAsync(id, code);
        }
    }
}
