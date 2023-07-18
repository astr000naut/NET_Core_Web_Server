using AutoMapper;
using DocumentFormat.OpenXml.EMMA;
using DocumentFormat.OpenXml.Office2010.Excel;
using MISA.WebFresher032023.Demo.BusinessLayer.Dtos.Input;
using MISA.WebFresher032023.Demo.BusinessLayer.Dtos.Output;
using MISA.WebFresher032023.Demo.Common.Resources;
using MISA.WebFresher032023.Demo.DataLayer;
using MISA.WebFresher032023.Demo.DataLayer.Entities.Input;
using MISA.WebFresher032023.Demo.DataLayer.Repositories;
using OfficeOpenXml.Style;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;

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
            var mKey = _unitOfWork.GetManipulationKey();
            try
            {
                _unitOfWork.SetManipulationKey(mKey + 1);
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
            var mKey = _unitOfWork.GetManipulationKey();
            try
            {
                _unitOfWork.SetManipulationKey(mKey + 1);
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
            var mKey = _unitOfWork.GetManipulationKey();
            try
            {
                _unitOfWork.SetManipulationKey(mKey + 1);
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
            var mKey = _unitOfWork.GetManipulationKey();
            try
            {
                _unitOfWork.SetManipulationKey(mKey + 1);
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
            var mKey = _unitOfWork.GetManipulationKey();
            try
            {
                _unitOfWork.SetManipulationKey(mKey + 1);
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
            var mKey = _unitOfWork.GetManipulationKey();
            try
            {
                _unitOfWork.SetManipulationKey(mKey + 1);
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
            var mKey = _unitOfWork.GetManipulationKey();
            try
            {
                _unitOfWork.SetManipulationKey(mKey + 1);
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

        public virtual async Task<byte[]> ExportExcelAsync(ExportExcelDto exportExcelDto)
        {
            var mKey = _unitOfWork.GetManipulationKey();
            try
            {
                _unitOfWork.SetManipulationKey(mKey + 1);
                await _unitOfWork.OpenAsync(mKey);

                var entityFilterDto = new EntityFilterDto();
                entityFilterDto.Skip = 0;
                entityFilterDto.KeySearch = exportExcelDto.KeySearch;

                var entityList = await FilterAsync(entityFilterDto);

                ExcelPackage excel = new ExcelPackage();

                var workSheet = excel.Workbook.Worksheets.Add("Sheet1");
                ExcelWorksheet ws = excel.Workbook.Worksheets[0];

                ws.Cells.Style.Font.Size = 13;
                ws.Cells.Style.Font.Name = "Times New Roman";
                ws.Rows.CustomHeight = false;
                ws.Cells.Style.Indent = 1;
                // Bật wrap text cho tất cả các cell
                ws.Cells.Style.WrapText = true;

                //Số lượng cột của header
                var countColHeader = exportExcelDto.Columns.Count;

                // merge các column lại từ col 1 đến số col header để tạo heading
                ws.Cells[1, 1].Value = exportExcelDto.TableHeading;
                ws.Cells[1, 1, 1, countColHeader].Merge = true;

                // in đậm heading
                ws.Cells[1, 1, 1, countColHeader].Style.Font.Bold = true;


                int colIndex = 1;
                int rowIndex = 2;

                // tạo các header 
                for (int i = 0; i < exportExcelDto.Columns.Count; ++i)
                {
                    var item = exportExcelDto.Columns[i];
                    var cell = ws.Cells[rowIndex, colIndex];

                    //set màu thành gray
                    var fill = cell.Style.Fill;
                    fill.PatternType = ExcelFillStyle.Solid;
                    fill.BackgroundColor.SetColor(System.Drawing.Color.LightGray);

                    //căn chỉnh các border
                    var border = cell.Style.Border;
                    border.Bottom.Style =
                        border.Top.Style =
                        border.Left.Style =
                        border.Right.Style = ExcelBorderStyle.Thin;

                    //Độ rộng của các cột
                    ws.Columns[i + 1].Width = item.Width;

                    // In đậm
                    ws.Cells[2, i + 1].Style.Font.Bold = true;

                    // align text
                    ws.Columns[i + 1].Style.HorizontalAlignment = item.Align switch
                    {
                        "left" => ExcelHorizontalAlignment.Left,
                        "right" => ExcelHorizontalAlignment.Right,
                        _ => ExcelHorizontalAlignment.Center,
                    };

                    cell.Value = item.Caption;
                    ++colIndex;
                }

                // căn giữa heading
                ws.Cells[1, 1, 1, countColHeader].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                // đổ dữ liệu vào sheet
                foreach (var entityDto in entityList.FilteredList)
                {
                    ++rowIndex;
                    colIndex = 1;
                    ws.Cells[rowIndex, colIndex++].Value = rowIndex - 2;

                    for (int i = 1; i < exportExcelDto.Columns.Count; i++)
                    {
                        var colValue = entityDto?.GetType().GetProperty(exportExcelDto.Columns[i].Name)?.GetValue(entityDto);
                        switch (exportExcelDto.Columns[i].Type)
                        {
                            case "number":
                                long value = (long)colValue;
                                ws.Cells[rowIndex, colIndex++].Value = value.ToString("N0", new CultureInfo("vi-VN")); ;
                                break;
                            case "date":
                                DateTime date = (DateTime)colValue;
                                ws.Cells[rowIndex, colIndex++].Value = date.ToString("dd/MM/yyyy");
                                break;
                            default:
                                ws.Cells[rowIndex, colIndex++].Value = colValue;
                                break;

                        }
                    }
                }

                // Thêm border cho các cells
                if (entityList.FilteredList.Count > 0)
                {
                    var cellBorder = ws.Cells[3, 1, 2 + entityList.FilteredList.Count, exportExcelDto.Columns.Count].Style.Border;
                    cellBorder.Bottom.Style =
                        cellBorder.Top.Style =
                        cellBorder.Left.Style =
                        cellBorder.Right.Style = ExcelBorderStyle.Thin;
                }

                byte[] bin = excel.GetAsByteArray();
                return bin;
            }
            finally
            {
                await _unitOfWork.CloseAsync(mKey);
            }

        }
    }
}
