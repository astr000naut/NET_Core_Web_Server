using MISA.WebFresher032023.Demo.Common.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WebFresher032023.Demo.DataLayer.Entities.Input
{
    // Base Input cho Proc thêm hoặc cập nhật thông tin nhân viên
    public abstract class BaseEmployeeInput
    {
        // ID nhân viên
        public Guid EmployeeId { get; set; }
        // Mã nhân viên
        public string EmployeeCode { get; set; }
        // Tên nhân viên
        public string EmployeeFullName { get; set; }
        // ID đơn vị
        public Guid DepartmentId { get; set; }
        // Vị trí
        public string PositionName { get; set; }
        // Ngày sinh
        public DateTime? DateOfBirth { get; set; }
        // Giá trị giới tính
        public Gender? Gender { get; set; }

        // Số CMND
        public string IdentityNumber { get; set; }
        // Ngày cấp CMND
        public DateTime? IdentityDate { get; set; }
        // Nơi cấp CMND
        public string IdentityPlace { get; set; }
        // Địa chỉ
        public string Address { get; set; }
        // Số ĐT
        public string PhoneNumber { get; set; }
        // Số ĐT cố định
        public string LandlineNumber { get; set; }
        // Email
        public string Email { get; set; }
        // Tài khoản ngân hàng
        public string BankAccount { get; set; }
        // Tên ngân hàng
        public string BankName { get; set; }
        // Chi nhánh ngân hàng
        public string BankBranch { get; set; }
    }
}
