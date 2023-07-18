using ClosedXML.Excel;
using Dapper;
using DocumentFormat.OpenXml.Spreadsheet;
using MISA.WebFresher032023.Demo.Common.Enum;
using MISA.WebFresher032023.Demo.Common.Enums;
using MISA.WebFresher032023.Demo.Common.Exceptions;
using MISA.WebFresher032023.Demo.Common.Resources;
using MISA.WebFresher032023.Demo.DataLayer;
using MISA.WebFresher032023.Demo.DataLayer.Entities.Input;
using MISA.WebFresher032023.Demo.DataLayer.Repositories;
using MISA.WebFresher032023.Demo.DataLayer.Repositories.AccountRepo;
using MISA.WebFresher032023.Demo.DataLayer.Repositories.ReceiptRepo;
using MISA.WebFresher032023.Demo.ExportService.Dtos.Input;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WebFresher032023.Demo.ExportService.Services
{
    public class ExportService : IExportService
    {
        private readonly IUnitOfWork _unitOfWork;

        public ExportService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<byte[]> ExportExcelAsync(ExportExcelInputDto exportExcelInputDto)
        {
            var mKey = _unitOfWork.GetManipulationKey();
            try
            {
                _unitOfWork.SetManipulationKey(mKey + 1);
                await _unitOfWork.OpenAsync(mKey);
                    
                // Tạo data table
                /*
                var dt = new DataTable
                {
                    TableName = exportExcelInputDto.Heading
                };

                for (int i = 0; i < exportExcelInputDto.Columns.Count; ++i)
                {
                    if (exportExcelInputDto.Columns[i].DataType != "datetime")
                        dt.Columns.Add(dt.Columns[i].ColumnName, typeof(DateTime));
                    else
                        dt.Columns.Add(dt.Columns[i].ColumnName, typeof(string));
                }
                */
                // Lấy data từ db
                var parameters = new DynamicParameters();
                // build query string
                var queryString = "SELECT ";
                for (int i = 0; i < exportExcelInputDto.Columns.Count; ++ i)
                {
                    var colName = exportExcelInputDto.Columns[i].ColumnName;
                    queryString += $"@Col{colName} ";
                    parameters.Add($"Col{colName}", colName);
                    if (i != exportExcelInputDto.Columns.Count - 1)
                        queryString += ", ";
                }
                queryString += $" FROM @Table{exportExcelInputDto.TableName};";
                parameters.Add($"Table{exportExcelInputDto.TableName}", exportExcelInputDto.TableName);
                
                var data = await _unitOfWork.Connection.QueryAsync(queryString, parameters);

                /*
                var filterParam = new EntityFilter()
                {
                    Skip = 0,
                    Take = null,
                    KeySearch = exportExcelInputDto.KeySearch,
                };
                */
                // Lấy danh sách nhân viên từ repository
                /*
                dynamic dataList = null;
                switch (exportExcelInputDto.TableName)
                {
                    case "customer":
                        dataList = await _customerRepository.FilterAsync(filterParam);
                        break;
                    case "account":
                        dataList = await _accountRepository.FilterAsync(filterParam);
                        break;
                    case "receipt":
                        dataList = await _receiptRepository.FilterAsync(filterParam);
                        break;
                }
                */

                /*
                // Số thứ tự
                int index = 0;

                // Thêm nhân viên vào data table
                foreach (var entity in dataList.ListData)
                {
                    if (entity == null) continue;

                    ++index;
                    dt.Rows.Add(index);
                    object[] rowVals = new object[exportExcelInputDto.Columns.Count];
                    rowVals[0] = index;

                    for (int i = 0; i < exportExcelInputDto.Columns.Count; ++ i)
                    {
                        rowVals[i] = entity.GetType().GetProperty(exportExcelInputDto.Columns[i].ColumnName).GetValue(entity, null);
                    }
                    dt.Rows.Add(rowVals);
                }

                var workbook = new XLWorkbook();
                // Thêm worksheet vào workbook
                var worksheet = workbook.AddWorksheet(exportExcelInputDto.Heading);

                // Tạo tiêu đề của các cột cho bảng thông tin nhân viên
                var headerRange = worksheet.Range("A3:K3");
                headerRange.Style.Fill.BackgroundColor = XLColor.LightGray;
                headerRange.Style.Font.Bold = true;

                for (int c = 'A'; c < 'A' + exportExcelInputDto.Columns.Count; ++ c)
                {
                    worksheet.Cell($"{c}3").Value = exportExcelInputDto.Columns[c - 'A'].ColumnName;
                }
                worksheet.Row(3).Height = 30;

                // Insert dữ liệu từ datatable vào worksheet
                worksheet.Cell("A4").InsertData(dt);
                */
                /*   // Thay đổi độ rộng của các cột

                   int[] colWidths = { 5, 20, 30, 10, 16, 20, 20, 30, 24, 18, 30 };

                   for (int i = 1; i <= 11; ++i)
                   {
                       worksheet.Column(i).Width = colWidths[i - 1];

                       // Chỉnh text align cho các cột
                       if (i != 5)
                       {
                           worksheet.Column(i).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Left;
                       }
                       else
                       {
                           // i == 5 : Cột ngày sinh thì căn giữa
                           worksheet.Column(i).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                       }
                   }

                   // Align các th của bảng
                   worksheet.Row(3).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                   worksheet.Row(3).Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;

                   // Thêm header 
                   var header = worksheet.Range("A1:K1").Merge();
                   header.Value = EmployeeExport.TableHeader;
                   header.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                   header.Style.Font.Bold = true;
                   header.Style.Font.FontSize = 16;

                   // Thêm border cho bảng
                   var tableRange = worksheet.Range("A3:K" + (employeeList.ListData.ToList().Count + 3));
                   tableRange.Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                   tableRange.Style.Border.OutsideBorderColor = XLColor.Black;
                   tableRange.Style.Border.InsideBorder = XLBorderStyleValues.Thin;
                   tableRange.Style.Border.InsideBorderColor = XLColor.Black

                   // Bật wrap text cho bảng
                   tableRange.Style.Alignment.WrapText = true;
                   */
                var stream = new MemoryStream();
                //workbook.SaveAs(stream);
                return stream.ToArray();
            }
            catch (Exception ex)
            {
                throw new InternalException(Error.ExportFail, ex.Message, Error.ExportFailMsg);
            }
            finally
            {
                await _unitOfWork.CloseAsync(mKey);
            }
        }
    }
}
