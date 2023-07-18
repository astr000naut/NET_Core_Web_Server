using AutoMapper;
using DocumentFormat.OpenXml.Drawing.Charts;
using MISA.WebFresher032023.Demo.BusinessLayer.Dtos.Output;
using MISA.WebFresher032023.Demo.Common.Enums;
using MISA.WebFresher032023.Demo.Common.Exceptions;
using MISA.WebFresher032023.Demo.DataLayer.Entities.Output;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using DataTable = System.Data.DataTable;
using ClosedXML.Excel;
using MISA.WebFresher032023.Demo.Common.Enum;
using MISA.WebFresher032023.Demo.Common.Resources;
using MISA.WebFresher032023.Demo.DataLayer.Entities.Input;
using MISA.WebFresher032023.Demo.DataLayer.Repositories;
using MISA.WebFresher032023.Demo.BusinessLayer.Dtos.Input;
using MISA.WebFresher032023.Demo.DataLayer;

namespace MISA.WebFresher032023.Demo.BusinessLayer.Services
{
    public class EmployeeService : BaseService<Employee, EmployeeDto, EmployeeInput, EmployeeInputDto>, IEmployeeService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IEmployeeRepository _employeeRepository;

        public EmployeeService(IEmployeeRepository employeeRepository, IMapper mapper, IUnitOfWork unitOfWork) : base(employeeRepository, mapper, unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _employeeRepository = employeeRepository;
        }


        /// <summary>
        /// Lấy mã nhân viên mới
        /// </summary>
        /// <returns>Mã nhân viên mới</returns>
        /// Author: DNT(26/05/2023)
        public async Task<string> GetNewCodeAsync()
        {
            var mKey = _unitOfWork.GetManipulationKey();
            try
            {
                _unitOfWork.SetManipulationKey(mKey + 1);
                await _unitOfWork.OpenAsync(mKey);
                var newCode = await _employeeRepository.GetNewCodeAsync();
                return newCode;
            } catch
            {
                throw;
            } finally
            {
                await _unitOfWork.CloseAsync(mKey);
            }
        }

        /// <summary>
        /// Tạo mới một nhân viên
        /// </summary>
        /// <param name="employeeCreateDto"></param>
        /// <returns>ID của nhân viên mới tạo</returns>
        /// <exception cref="ConflictException"></exception>
        /// Author: DNT(26/05/2023)
        /// Modified: DNT(09/06/2023)
        public override async Task<Guid?> CreateAsync(EmployeeInputDto employeeInputDto)
        {
            var mKey = _unitOfWork.GetManipulationKey();
            try
            {
                _unitOfWork.SetManipulationKey(mKey + 1);
                await _unitOfWork.OpenAsync(mKey);
                await _unitOfWork.BeginAsync(mKey);

                // Kiểm tra đơn vị có tồn tại
                var isDepartmentIdValid = await _employeeRepository.ValidateDepartmentId(employeeInputDto.DepartmentId);
                if (!isDepartmentIdValid)
                {
                    throw new ConflictException(Error.ConflictCode, Error.InvalidDepartmentIdMsg, Error.InvalidDepartmentIdMsg);
                }
                // Kiểm tra mã đã tồn tại
                var isEmployeeCodeExist = await _baseRepository.CheckCodeExistAsync(null, employeeInputDto.EmployeeCode);
                if (isEmployeeCodeExist)
                {
                    throw new ConflictException(Error.ConflictCode, Error.EmployeeCodeHasExistMsg, Error.EmployeeCodeHasExistMsg);
                }
                var result = await base.CreateAsync(employeeInputDto);
                await _unitOfWork.CommitAsync(mKey);
                return result;

            } catch
            {
                throw;  
            } finally
            {
                await _unitOfWork.DisposeAsync(mKey);
                await _unitOfWork.CloseAsync(mKey);
            }
        }

        /// <summary>
        /// Cập nhật thông tin nhân viên
        /// </summary>
        /// <param name="id">ID của nhân viên</param>
        /// <param name="employeeInputDto"></param>
        /// <returns>Giá trị boolean biểu thị việc cập nhật thành công hay không</returns>
        /// <exception cref="ConflictException"></exception>
        /// Author: DNT(26/05/2023)
        /// Modified: DNT(09/06/2023)
        public override async Task<bool> UpdateAsync(Guid id, EmployeeInputDto employeeInputDto)
        {
            var mKey = _unitOfWork.GetManipulationKey();
            try
            {
                _unitOfWork.SetManipulationKey(mKey + 1);
                await _unitOfWork.OpenAsync(mKey);
                await _unitOfWork.BeginAsync(mKey);

                // Kiểm tra nhân viên có tồn tại
                _ = await _employeeRepository.GetAsync(id) ?? throw new ConflictException(Error.ConflictCode, Error.InvalidEmployeeIdMsg, Error.InvalidEmployeeIdMsg);

                // Kiểm tra đơn vị có tồn tại
                var isDepartmentIdValid = await _employeeRepository.ValidateDepartmentId(employeeInputDto.DepartmentId);
                if (!isDepartmentIdValid)
                {
                    throw new ConflictException(Error.ConflictCode, Error.InvalidDepartmentIdMsg, Error.InvalidDepartmentIdMsg);
                }
                // Kiểm tra mã đã tồn tại
                var isEmployeeCodeExist = await _baseRepository.CheckCodeExistAsync(id, employeeInputDto.EmployeeCode);
                if (isEmployeeCodeExist)
                {
                    throw new ConflictException(Error.ConflictCode, Error.EmployeeCodeHasExistMsg, Error.EmployeeCodeHasExistMsg);
                }

                // Cập nhật thông tin nhân viên 
                var result = await base.UpdateAsync(id, employeeInputDto);
                await _unitOfWork.CommitAsync(mKey);
                return result;

            } catch
            {
                throw;
            } finally
            {
                await _unitOfWork.DisposeAsync(mKey);
                await _unitOfWork.CloseAsync(mKey);
            }
        }

        /// <summary>
        /// Tạo file excel thông tin nhân viên
        /// </summary>
        /// <returns>File Excel được chuyển sang dạng byte[]</returns>
        /// Author: DNT(06/06/2023)
        public async Task<byte[]> ExportEmployeesToExcelAsync()
        {
            var mKey = _unitOfWork.GetManipulationKey();
            try
            {
                _unitOfWork.SetManipulationKey(mKey + 1);
                await _unitOfWork.OpenAsync(mKey);
                // Tạo data table
                var dt = new DataTable
                {
                    TableName = EmployeeExport.TableName
                };

                // Dựng cấu trúc của data table
                dt.Columns.Add(EmployeeExport.Col_1);
                dt.Columns.Add(EmployeeExport.Col_2);
                dt.Columns.Add(EmployeeExport.Col_3);
                dt.Columns.Add(EmployeeExport.Col_4);
                dt.Columns.Add(EmployeeExport.Col_5);
                dt.Columns.Add(EmployeeExport.Col_6);
                dt.Columns.Add(EmployeeExport.Col_7);
                dt.Columns.Add(EmployeeExport.Col_8);
                dt.Columns.Add(EmployeeExport.Col_9);
                dt.Columns.Add(EmployeeExport.Col_10);
                dt.Columns.Add(EmployeeExport.Col_11);

                // Tạo filter param
                var employeeFilter = new EntityFilter()
                {
                    Skip = 0,
                    Take = null,
                    KeySearch = null,
                };
                // Lấy danh sách nhân viên từ repository
                var employeeList = await _employeeRepository.FilterAsync(employeeFilter);

                // Số thứ tự của nhân viên
                int index = 0;

                // Thêm nhân viên vào data table
                foreach (var employee in employeeList.ListData)
                {
                    if (employee == null) continue;

                    ++index;
                    var genderName = employee.Gender switch
                    {
                        Gender.Male => "Nam",
                        Gender.Female => "Nữ",
                        Gender.Other => "Khác",
                        _ => ""
                    };
                    dt.Rows.Add(
                        index,
                        employee.EmployeeCode,
                        employee.EmployeeFullName,
                        genderName,
                        employee.DateOfBirth,
                        employee.IdentityNumber,
                        employee.PositionName,
                        employee.DepartmentName,
                        employee.BankAccount,
                        employee.BankName,
                        employee.BankBranch
                    );
                }

                var workbook = new XLWorkbook();
                // Thêm worksheet vào workbook
                var worksheet = workbook.AddWorksheet(EmployeeExport.WorksheetName);

                // Tạo tiêu đề của các cột cho bảng thông tin nhân viên
                var headerRange = worksheet.Range("A3:K3");
                headerRange.Style.Fill.BackgroundColor = XLColor.LightGray;
                headerRange.Style.Font.Bold = true;

                worksheet.Cell("A3").Value = EmployeeExport.Col_1;
                worksheet.Cell("B3").Value = EmployeeExport.Col_2;
                worksheet.Cell("C3").Value = EmployeeExport.Col_3;
                worksheet.Cell("D3").Value = EmployeeExport.Col_4;
                worksheet.Cell("E3").Value = EmployeeExport.Col_5;
                worksheet.Cell("F3").Value = EmployeeExport.Col_6;
                worksheet.Cell("G3").Value = EmployeeExport.Col_7;
                worksheet.Cell("H3").Value = EmployeeExport.Col_8;
                worksheet.Cell("I3").Value = EmployeeExport.Col_9;
                worksheet.Cell("J3").Value = EmployeeExport.Col_10;
                worksheet.Cell("K3").Value = EmployeeExport.Col_11;
                worksheet.Row(3).Height = 30;

                // Insert dữ liệu từ datatable vào worksheet
                worksheet.Cell("A4").InsertData(dt);

                // Thay đổi độ rộng của các cột

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
                tableRange.Style.Border.InsideBorderColor = XLColor.Black;

                // Bật wrap text cho bảng
                tableRange.Style.Alignment.WrapText = true;

                var stream = new MemoryStream();
                workbook.SaveAs(stream);
                return stream.ToArray();
            }
            catch (Exception ex)
            {
                throw new InternalException(Error.ExportFail, ex.Message, Error.ExportFailMsg);
            } finally
            {
                await _unitOfWork.CloseAsync(mKey);
            }
        }
    }
}
