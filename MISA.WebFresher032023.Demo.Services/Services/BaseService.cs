﻿using AutoMapper;
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
        /// Modified: DNT(09/06/2023)
        public virtual async Task<Guid?> CreateAsync(TEntityCreateDto tEntityCreateDto)
        {

            var entityCreate = _mapper.Map<TEntityCreate>(tEntityCreateDto);
            Type type = typeof(TEntityCreate);
            var entityName = typeof(TEntity).Name;
            var newId = Guid.NewGuid();

            var idProperty = type.GetProperty($"{entityName}Id");
            idProperty?.SetValue(entityCreate, newId);

            var createdDateProperty = type.GetProperty("CreatedDate");
            createdDateProperty?.SetValue(entityCreate, DateTime.Now.ToLocalTime());

            var createdByProperty = type.GetProperty("CreatedBy");
            createdByProperty?.SetValue(entityCreate, "Dux");

            var isCreated = await _baseRepository.CreateAsync(entityCreate);

            return isCreated ? newId : null;

        }

        /// <summary>
        /// Cập nhật thông tin một Entity
        /// </summary>
        /// <param name="id"></param>
        /// <param name="tEntityUpdateDto"></param>
        /// <returns></returns>
        /// Author: DNT(26/05/2023)
        /// Modified: DNT(09/06/2023)
        public virtual async Task<bool> UpdateAsync(Guid id, TEntityUpdateDto tEntityUpdateDto)
        {
            var entityUpdate = _mapper.Map<TEntityUpdate>(tEntityUpdateDto);

            Type type = typeof(TEntityUpdate);
            var entityName = typeof(TEntity).Name;

     
            var idProperty = type.GetProperty($"{entityName}Id");
            idProperty?.SetValue(entityUpdate, id);

            var modifiedDateProperty = type.GetProperty("ModifiedDate");
            modifiedDateProperty?.SetValue(entityUpdate, DateTime.Now.ToLocalTime());

            var modifiedByProperty = type.GetProperty("ModifiedBy");
            modifiedByProperty?.SetValue(entityUpdate, "Dux");

            return await _baseRepository.UpdateAsync(id, entityUpdate);
        }

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
            // Map từ EntityFilterDto sang EntityFilter
            var entityFilter = _mapper.Map<EntityFilter>(entityFilterDto);

            // Lấy dữ liệu từ Repository 
            var tEntityFilteredList = await _baseRepository.FilterAsync(entityFilter);

            // Khởi tạo kêt quả trả về
            var tEntityFilteredListDto = new FilteredListDto<TEntityDto>
            {
                TotalRecord = tEntityFilteredList.TotalRecord,
                FilteredList = new List<TEntityDto?>()
            };

            // Map dữ liệu nhận được từ Repository sang Dto
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
