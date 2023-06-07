using AutoMapper;
using DocumentFormat.OpenXml.Drawing.Charts;
using MISA.WebFresher032023.Demo.BusinessLayer.Dtos.Input;
using MISA.WebFresher032023.Demo.BusinessLayer.Dtos.Output;
using MISA.WebFresher032023.Demo.Common.Enums;
using MISA.WebFresher032023.Demo.Common.Exceptions;
using MISA.WebFresher032023.Demo.DataLayer.Entities.Input;
using MISA.WebFresher032023.Demo.DataLayer.Entities.Output;
using MISA.WebFresher032023.Demo.DataLayer.Repositories;
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

namespace MISA.WebFresher032023.Demo.BusinessLayer.Services
{
    public class EmployeeService : BaseService<Employee, EmployeeDto, EmployeeCreate, EmployeeCreateDto, EmployeeUpdate, EmployeeUpdateDto>, IEmployeeService
    {
        private readonly IEmployeeRepository _employeeRepository;

        public EmployeeService(IEmployeeRepository employeeRepository, IMapper mapper) : base(employeeRepository, mapper) {
            _employeeRepository = employeeRepository;
        }


        /// <summary>
        /// Lấy mã nhân viên mới
        /// </summary>
        /// <returns></returns>
        /// Author: DNT(26/05/2023)
        public async Task<string> GetNewCodeAsync()
        {
            var newCode = await _employeeRepository.GetNewCodeAsync();
            return newCode;
        }

        /// <summary>
        /// Tạo mới một nhân viên
        /// </summary>
        /// <param name="employeeCreateDto"></param>
        /// <returns></returns>
        /// <exception cref="ConflictException"></exception>
        /// Author: DNT(26/05/2023)
        public override async Task<Guid?> CreateAsync(EmployeeCreateDto employeeCreateDto)
        {
            // Kiểm tra đơn vị có tồn tại
            var isDepartmentIdValid = await _employeeRepository.ValidateDepartmentId(employeeCreateDto.DepartmentId);
            if (!isDepartmentIdValid)
            {
                throw new ConflictException(Error.ConflictCode, Error.InvalidDepartmentId, Error.InvalidDepartmentId);
            }
            // Kiểm tra mã đã tồn tại
            var isEmployeeCodeExist = await _baseRepository.CheckCodeExistAsync(null, employeeCreateDto.EmployeeCode);
            if (isEmployeeCodeExist)
            {
                throw new ConflictException(Error.ConflictCode, Error.EmployeeCodeHasExist, Error.EmployeeCodeHasExist);
            }

            // Tạo mới nhân viên 
             var employeeCreate = _mapper.Map<EmployeeCreate>(employeeCreateDto);
             employeeCreate.EmployeeId = Guid.NewGuid();
             employeeCreate.CreatedDate = DateTime.Now.ToLocalTime();
             employeeCreate.CreatedBy = "Dux";
             var isCreated = await _employeeRepository.CreateAsync(employeeCreate);

             return isCreated ? employeeCreate.EmployeeId : null;
        }

        /// <summary>
        /// Cập nhật thông tin nhân viên
        /// </summary>
        /// <param name="id"></param>
        /// <param name="employeeUpdateDto"></param>
        /// <returns></returns>
        /// <exception cref="ConflictException"></exception>
        /// Author: DNT(26/05/2023)
        public override async Task<bool> UpdateAsync(Guid id, EmployeeUpdateDto employeeUpdateDto)
        {
            _ = await _employeeRepository.GetAsync(id) ?? throw new ConflictException(Error.ConflictCode, Error.InvalidEmployeeId, Error.InvalidEmployeeId);

            // Kiểm tra đơn vị có tồn tại
            var isDepartmentIdValid = await _employeeRepository.ValidateDepartmentId(employeeUpdateDto.DepartmentId);
            if (!isDepartmentIdValid)
            {
                throw new ConflictException(Error.ConflictCode, Error.InvalidDepartmentId, Error.InvalidDepartmentId);
            }
            // Kiểm tra mã đã tồn tại
            var isEmployeeCodeExist = await _baseRepository.CheckCodeExistAsync(id, employeeUpdateDto.EmployeeCode);
            if (isEmployeeCodeExist)
            {
                throw new ConflictException(Error.ConflictCode, Error.EmployeeCodeHasExist, Error.EmployeeCodeHasExist);
            }

            // Cập nhật thông tin nhân viên 
            var employeeUpdate = _mapper.Map<EmployeeUpdate>(employeeUpdateDto);
            employeeUpdate.EmployeeId = id;
            employeeUpdate.ModifiedDate = DateTime.Now.ToLocalTime();
            employeeUpdate.ModifiedBy = "Dux";
            return await _employeeRepository.UpdateAsync(id, employeeUpdate);
        }

        public async Task<byte[]> ExportEmployeesToExcelAsync()
        {
            var dt = new DataTable
            {
                TableName = "Employee Table"
            };
            // Truyền field cần export từ front end, truyền limit, offset,...
            dt.Columns.Add("STT", typeof(int));
            dt.Columns.Add("Mã nhân viên", typeof(string));
            dt.Columns.Add("Tên nhân viên", typeof(string));
            dt.Columns.Add("Giới tính", typeof(string));
            dt.Columns.Add("Ngày sinh", typeof(DateTime));
            dt.Columns.Add("Số CMND", typeof(string));
            dt.Columns.Add("Chức danh", typeof(string));
            dt.Columns.Add("Tên đơn vị", typeof(string));
            dt.Columns.Add("Số tài khoản", typeof(string));
            dt.Columns.Add("Tên ngân hàng", typeof(string));
            dt.Columns.Add("Chi nhánh TK ngân hàng", typeof(string));

            var employeeFilter = new EntityFilter()
            {
                Skip = 0,
                Take = null,
                KeySearch = null,
            };
            var employeeList = await _employeeRepository.FilterAsync(employeeFilter);

            int index = 0;
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
            var worksheet = workbook.AddWorksheet(dt, "Danh sách nhân sự");
            int[] colWidths = { 10, 20, 30, 10, 16, 20, 20, 40, 24, 18, 30 };
            for (char  col = 'A'; col <= (char) 'A' + 10;  ++col)
            {
                worksheet.Column(col.ToString()).Width = colWidths[(int)col - 'A'];
                worksheet.Column(col.ToString()).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Left;
            }
            worksheet.Column("A").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;

            var stream = new MemoryStream();
            workbook.SaveAs(stream);
            return stream.ToArray();
        }
    }
}
