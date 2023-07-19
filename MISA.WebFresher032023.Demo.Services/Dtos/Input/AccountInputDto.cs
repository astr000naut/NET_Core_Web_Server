using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WebFresher032023.Demo.BusinessLayer.Dtos.Input
{
    // DTO thêm/sửa tài khoản
    public class AccountInputDto
    {
        // id tài khoản
        public Guid? AccountId { get; set; }

        // số tài khoản
        [StringLength(50)]
        public string AccountNumber { get; set; }

        // tên tài khoản 
        [StringLength(255)]
        public string AccountNameVi { get; set; }

        // tên tài khoản tiếng Anh
        [StringLength(255)]
        public string AccountNameEn { get; set; }

        //số tài khoản tổng hợp
        [StringLength(50)]
        public string? ParentNumber { get; set; }

        //id của tài khoản tổng hợp
        public Guid? ParentId { get; set; }

        // Loại tài khoản enum
        public int CategoryKind { get; set; }
        // loại tài khoản
        [StringLength(100)]
        public string CategoryKindName { get; set; }

        // diễn giải
        [StringLength(255)]
        public string Description { get; set; }

        // chi tiết theo tài khoản ngân hàng
        public bool? DetailByBankAccount { get; set; }

        // có chi tiết theo đối tượng hay không
        public bool? DetailByAccountObject { get; set; }

        // chi tiết theo đối tượng enum
        public int? DetailByAccountObjectKind { get; set; }

        // có hạch toán ngoại tệ
        public bool? ForeignCurrencyAccounting { get; set; }

        //trạng thái sử dụng enum
        public bool? UsingStatus { get; set; }

        // trạng thái sử dụng
        public string UsingStatusName { get; set; }

    }
}
