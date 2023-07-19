using AutoMapper;
using DocumentFormat.OpenXml.Office2010.Excel;
using DocumentFormat.OpenXml.Office2010.Word;
using DocumentFormat.OpenXml.Spreadsheet;
using MISA.WebFresher032023.Demo.BusinessLayer.Dtos.Input;
using MISA.WebFresher032023.Demo.BusinessLayer.Dtos.Output;
using MISA.WebFresher032023.Demo.Common.Enums;
using MISA.WebFresher032023.Demo.Common.Exceptions;
using MISA.WebFresher032023.Demo.DataLayer;
using MISA.WebFresher032023.Demo.DataLayer.Entities.Input;
using MISA.WebFresher032023.Demo.DataLayer.Entities.Output;
using MISA.WebFresher032023.Demo.DataLayer.Repositories;
using MISA.WebFresher032023.Demo.DataLayer.Repositories.AccountRepo;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WebFresher032023.Demo.BusinessLayer.Services.AccountSvc
{
    public class AccountService : BaseService<Account, AccountDto, AccountInput, AccountInputDto>, IAccountService
    {

        private readonly IUnitOfWork _unitOfWork;
        private readonly IAccountRepository _accountRepository;
        
        public AccountService(IAccountRepository accountRepository, IMapper mapper, IUnitOfWork unitOfWork) : base(accountRepository, mapper, unitOfWork)
        {
            _accountRepository = accountRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<FilteredListDto<AccountDto>> FilterAccountAsync(AccountFilterInputDto accountFilterInputDto)
        {
            var mKey = _unitOfWork.GetManipulationKey();
            try
            {
                _unitOfWork.SetManipulationKey(mKey + 1);
                await _unitOfWork.OpenAsync(mKey);

                var accountFilterInput = _mapper.Map<AccountFilterInput>(accountFilterInputDto);

                // Lấy dữ liệu từ Repository 
                var accountFilteredList = await _accountRepository.FilterAccountAsync(accountFilterInput);

                // Khởi tạo kêt quả trả về
                var filteredListDto = new FilteredListDto<AccountDto>
                {
                    TotalRecord = accountFilteredList.TotalRecord,
                    FilteredList = new List<AccountDto?>()
                };

                // Map dữ liệu nhận được từ Repository sang Dto
                foreach (var account in accountFilteredList.ListData)
                {
                    var accountDto = _mapper.Map<AccountDto>(account);
                    filteredListDto.FilteredList.Add(accountDto);
                }

                return filteredListDto;
            }
            catch
            {
                throw;
            } finally
            {
                await _unitOfWork.CloseAsync(mKey);
            }
            
        }

        public override async Task<bool> DeleteByIdAsync(Guid id)
        {
            var mKey = _unitOfWork.GetManipulationKey();
            try
            {
                _unitOfWork.SetManipulationKey(mKey + 1);
                await _unitOfWork.OpenAsync(mKey);
                await _unitOfWork.BeginAsync(mKey);

                var account = await _accountRepository.GetAsync(id);
                if (account.IsParent)
                    throw new ConflictException(Error.ConflictCode, Error.AccountDeleteConflictMsg, Error.AccountDeleteConflictMsg);

                var result = await _accountRepository.DeleteByIdAsync(id);
                await _unitOfWork.CommitAsync(mKey);
                return result;

            } catch
            {
                throw;
            }
            finally
            {
                await _unitOfWork.DisposeAsync(mKey);
                await _unitOfWork.CloseAsync(mKey);
            }

        }

        public override async Task<byte[]> ExportExcelAsync(ExportExcelDto exportExcelDto)
        {
            var mKey = _unitOfWork.GetManipulationKey();
            try
            {
                _unitOfWork.SetManipulationKey(mKey + 1);
                await _unitOfWork.OpenAsync(mKey);

                var accountFilterInputDto = new AccountFilterInputDto();
                accountFilterInputDto.Skip = 0;
                accountFilterInputDto.KeySearch = exportExcelDto.KeySearch;
                var accountList = await FilterAccountAsync(accountFilterInputDto);

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
                foreach (var accountDto in accountList.FilteredList)
                {
                    ++rowIndex;
                    colIndex = 1;
                    ws.Cells[rowIndex, colIndex++].Value = rowIndex - 2;

                    for (int i = 1; i < exportExcelDto.Columns.Count; i++)
                    {
                        var colValue = accountDto?.GetType().GetProperty(exportExcelDto.Columns[i].Name)?.GetValue(accountDto);
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
                if (accountList.FilteredList.Count > 0)
                {
                    var cellBorder = ws.Cells[3, 1, 2 + accountList.FilteredList.Count, exportExcelDto.Columns.Count].Style.Border;
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

        public async Task<bool> ChangeUsingStatusAsync(AccountChangeUsingStatusDto accountChangeUsingStatusDto)
        {
            var mKey = _unitOfWork.GetManipulationKey();
            try
            {
                _unitOfWork.SetManipulationKey(mKey + 1);
                await _unitOfWork.OpenAsync(mKey);
                await _unitOfWork.BeginAsync(mKey);
                var mCodeId = accountChangeUsingStatusDto.MCodeId;
                var usingStatus = accountChangeUsingStatusDto.UsingStatus;
                var result = await _accountRepository.ChangeUsingStatusAsync(mCodeId, usingStatus);
                await _unitOfWork.CommitAsync(mKey);
                return result;

            } catch
            {
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
