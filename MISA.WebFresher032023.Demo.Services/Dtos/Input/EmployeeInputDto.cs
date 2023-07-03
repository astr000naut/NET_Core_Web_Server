using MISA.WebFresher032023.Demo.Common.Enum;
using MISA.WebFresher032023.Demo.Common.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WebFresher032023.Demo.BusinessLayer.Dtos.Input
{
    // Base DTO cho Employee
    public class EmployeeInputDto
    {
        // Mã nhân viên Varchar(50)
        [Required]
        [StringLength(50)]
        public string EmployeeCode { get; set; }

        // Tên nhân viên Varchar(100)
        [Required]
        [StringLength(100)]
        public string EmployeeFullName { get; set; }

        // ID đơn vị Char(36)
        public Guid DepartmentId { get; set; }

        // Vị trí Varchar(255)
        [StringLength(255)]
        public string? PositionName { get; set; }

        // Ngày sinh Date
        public DateTime? DateOfBirth { get; set; }

        // Giới tính Tiny Int 0 - 1 - 2
        [Range(0, 2)]
        public int? Gender { get; set; }

        // Số CMND Varchar(25)
        [StringLength(25)]
        public string? IdentityNumber { get; set; }

        // Ngày cấp Date
        public DateTime? IdentityDate { get; set; }

        // Nơi cấp Varchar(255)
        [StringLength(255)]
        public string? IdentityPlace { get; set; }

        // Địa chỉ Varchar(255)
        [StringLength(255)]
        public string? Address { get; set; }

        // Số ĐT di động varchar(50)
        [StringLength(50)]
        public string? PhoneNumber { get; set; }

        // Số ĐT cố định varchar(50)
        [StringLength(50)]
        public string? LandlineNumber { get; set; }

        // Địa chỉ Email varchar(50)
        [StringLength(50)]
        public string? Email { get; set; }

        // Số tài khoản varchar(50)
        [StringLength(50)]
        public string? BankAccount { get; set; }

        // Tên ngân hàng varchar(255)
        [StringLength(255)]
        public string? BankName { get; set; }

        // Chi nhánh varchar(255)
        [StringLength(255)]
        public string? BankBranch { get; set; }
    }
}
