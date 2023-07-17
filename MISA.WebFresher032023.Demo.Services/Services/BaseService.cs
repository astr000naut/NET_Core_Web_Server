using AutoMapper;
using DocumentFormat.OpenXml.EMMA;
using DocumentFormat.OpenXml.Office2010.Excel;
using MISA.WebFresher032023.Demo.BusinessLayer.Dtos.Input;
using MISA.WebFresher032023.Demo.BusinessLayer.Dtos.Output;
using MISA.WebFresher032023.Demo.Common.Resources;
using MISA.WebFresher032023.Demo.DataLayer;
using MISA.WebFresher032023.Demo.DataLayer.Entities.Input;
using MISA.WebFresher032023.Demo.DataLayer.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WebFresher032023.Demo.BusinessLayer.Services
{
    public abstract class BaseService<TEntity, TEntityDto, TEntityInput, TEntityInputDto>
        : IBaseService<TEntityDto, TEntityInputDto>
    {
        protected readonly IBaseRepository<TEntity, TEntityInput> _baseRepository;
        private IUnitOfWork _unitOfWork;
        protected readonly IMapper _mapper;

        public BaseService(IBaseRepository<TEntity, TEntityInput> repository, IMapper mapper, IUnitOfWork unitOfWork)
        {
            _baseRepository = repository;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        /// <summary>
        /// Tạo một Entity
        /// </summary>
        /// <param name="tEntityInputDto"></param>
        /// <returns>ID của engity mới được tạo</returns>
        /// Author: DNT(26/05/2023)
        /// Modified: DNT(09/06/2023)
        public virtual async Task<Guid?> CreateAsync(TEntityInputDto tEntityInputDto)
        {
            var mKey = _unitOfWork.getManipulationKey();
            try
            {
                _unitOfWork.setManipulationKey(mKey + 1);
                await _unitOfWork.OpenAsync(mKey);
                await _unitOfWork.BeginAsync(mKey);
                var entityInput = _mapper.Map<TEntityInput>(tEntityInputDto);
                Type type = typeof(TEntityInput);
                var entityName = typeof(TEntity).Name;
                var newId = Guid.NewGuid();

                var idProperty = type.GetProperty($"{entityName}Id");
                idProperty?.SetValue(entityInput, newId);

                var createdDateProperty = type.GetProperty("CreatedDate");
                createdDateProperty?.SetValue(entityInput, DateTime.Now.ToLocalTime());

                var createdByProperty = type.GetProperty("CreatedBy");
                createdByProperty?.SetValue(entityInput, Value.CreatedBy);

                var isCreated = await _baseRepository.CreateAsync(entityInput);

                await _unitOfWork.CommitAsync(mKey);
                return isCreated ? newId : null;

            } catch
            {
                await _unitOfWork.RollbackAsync(mKey);
                throw;
            } finally
            {
                await _unitOfWork.DisposeAsync(mKey);
                await _unitOfWork.CloseAsync(mKey);
            }

        }

        /// <summary>
        /// Cập nhật thông tin một Entity
        /// </summary>
        /// <param name="id"></param>
        /// <param name="tEntityInputDto"></param>
        /// <returns>Giá trị boolean biểu thị entity đã được cập nhật</returns>
        /// Author: DNT(26/05/2023)
        /// Modified: DNT(09/06/2023)
        public virtual async Task<bool> UpdateAsync(Guid id, TEntityInputDto tEntityInputDto)
        {
            var mKey = _unitOfWork.getManipulationKey();
            try
            {
                _unitOfWork.setManipulationKey(mKey + 1);
                await _unitOfWork.OpenAsync(mKey);
                await _unitOfWork.BeginAsync(mKey);
                var entityInput = _mapper.Map<TEntityInput>(tEntityInputDto);

                Type type = typeof(TEntityInput);
                var entityName = typeof(TEntity).Name;


                var idProperty = type.GetProperty($"{entityName}Id");
                idProperty?.SetValue(entityInput, id);

                var modifiedDateProperty = type.GetProperty("ModifiedDate");
                modifiedDateProperty?.SetValue(entityInput, DateTime.Now.ToLocalTime());

                var modifiedByProperty = type.GetProperty("ModifiedBy");
                modifiedByProperty?.SetValue(entityInput, Value.ModifiedBy);

                var result = await _baseRepository.UpdateAsync(entityInput);
                await _unitOfWork.CommitAsync(mKey);    
                return result;
            } catch
            {
                await _unitOfWork.RollbackAsync(mKey);
                throw;

            } finally
            {
                await _unitOfWork.DisposeAsync(mKey);
                await _unitOfWork.CloseAsync(mKey); 
            }
        }

        /// <summary>
        /// Lấy một Entity theo ID
        /// </summary>
        /// <param name="id">ID của entity</param>
        /// <returns>Entity DTO chứa thông tin của entity</returns>
        /// Author: DNT(26/05/2023)
        public virtual async Task<TEntityDto?> GetAsync(Guid id)
        {
            var mKey = _unitOfWork.getManipulationKey();
            try
            {
                _unitOfWork.setManipulationKey(mKey + 1);
                await _unitOfWork.OpenAsync(mKey);
                var entity = await _baseRepository.GetAsync(id);
                var entityDto = _mapper.Map<TEntityDto>(entity);
                return entityDto;
            } catch
            {
                throw;
            } finally
            {
                await _unitOfWork.CloseAsync(mKey);
            }
        }

        /// <summary>
        /// Filter danh sách Entity
        /// </summary>
        /// <param name="entityFilterDto"></param>
        /// <returns>FilteredListDto chứa danh sách EntityDto tìm được</returns>
        /// Author: DNT(29/05/2023)
        public async Task<FilteredListDto<TEntityDto>> FilterAsync(EntityFilterDto entityFilterDto)
        {
            var mKey = _unitOfWork.getManipulationKey();
            try
            {
                _unitOfWork.setManipulationKey(mKey + 1);
                await _unitOfWork.OpenAsync(mKey);
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

            } catch {
                throw;
            }
             finally
            {
                await _unitOfWork.CloseAsync(mKey);
            }
        }

        /// <summary>
        /// Xóa một Entity theo ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns>true - false : xóa thành công hay không</returns>
        /// Author: DNT(26/05/2023)
        public virtual async Task<bool> DeleteByIdAsync(Guid id)
        {
            var mKey = _unitOfWork.getManipulationKey();
            try
            {
                _unitOfWork.setManipulationKey(mKey + 1);
                await _unitOfWork.OpenAsync(mKey);
                await _unitOfWork.BeginAsync(mKey);
                var result = await _baseRepository.DeleteByIdAsync(id);
                await _unitOfWork.CommitAsync(mKey);
                return result;
            }
            catch {
                await _unitOfWork.RollbackAsync(mKey);
                throw; 
            }
            finally {
                await _unitOfWork.DisposeAsync(mKey);
                await _unitOfWork.CloseAsync(mKey);
            }
        }

        /// <summary>
        /// Kiểm tra trùng mã 
        /// </summary>
        /// <param name="id">id của entity (chỉ yêu cầu trong trường hợp cập nhật)</param>
        /// <param name="code">Mã</param>
        /// <returns>true - false : có bị trùng hay không</returns>
        /// Author: DNT(27/05/2023)
        public async Task<bool> CheckCodeExistAsync(Guid? id, string code)
        {
            var mKey = _unitOfWork.getManipulationKey();
            try
            {
                _unitOfWork.setManipulationKey(mKey + 1);
                await _unitOfWork.OpenAsync(mKey);
                var result = await _baseRepository.CheckCodeExistAsync(id, code);
                return result;
            }
            catch
            {
                throw;
            }
            finally
            {
                await _unitOfWork.CloseAsync(mKey);
            }
            
        }

        /// <summary>
        /// Xóa nhiều Entity
        /// </summary>
        /// <param name="entityIdList">Mảng ID của Entity cần xóa</param>
        /// <returns>Số entity đã xóa thành công</returns>
        /// Author: DNT(26/05/2023)
        public async Task<int> DeleteMultipleAsync(List<Guid> entityIdList)
        {
            var mKey = _unitOfWork.getManipulationKey();
            try
            {
                _unitOfWork.setManipulationKey(mKey + 1);
                await _unitOfWork.OpenAsync(mKey);
                await _unitOfWork.BeginAsync(mKey);

                // Transform list to string
                var stringIdList = string.Join(",", entityIdList);
                var result = await _baseRepository.DeleteMultipleAsync(stringIdList);
                _unitOfWork.Commit(mKey);
                return result;
            }
            catch
            {
                await _unitOfWork.RollbackAsync(mKey);
                throw;
            }
            finally
            {
                await _unitOfWork.DisposeAsync(mKey);
                await _unitOfWork.CloseAsync(mKey);
            }

        }
    }
}
