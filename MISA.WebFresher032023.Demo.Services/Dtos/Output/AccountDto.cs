using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WebFresher032023.Demo.BusinessLayer.Dtos.Output
{
    public class AccountDto
    {
        // id tài khoản
        public Guid AccountId { get; set; }

        // số tài khoản
        public string AccountNumber { get; set; }

        // tên tài khoản
        public string AccountNameVi { get; set; }

        // tên tiếng anh
        public string? AccountNameEn { get; set; }

        // là tài khoản cha
        public bool IsParent { get; set; }

        // id cảu tài khoản cha
        public Guid? ParentId { get; set; }

        // số tài khoản cha
        public string? ParentNumber { get; set; }

        // loại tài khoản
        public int CategoryKind { get; set; }

        // tên loại tài khoản
        public string CategoryKindName { get; set; }

        // diễn giải
        public string? Description { get; set; }

        // chi tiết theo tài khoản ngân hàng
        public bool? DetailByBankAccount { get; set; }

        // chi tiết theo đối tượng
        public bool? DetailByAccountObject { get; set; }

        // chi tiêt theo đối tượng loại
        public int? DetailByAccountObjectKind { get; set; }

        // bậc của tài khoản
        public int Grade { get; set; }

        // mCodeId để tạo cấu trúc cây
        public string MCodeId { get; set; }

        // có hạch toán ngoại tệ
        public bool? ForeignCurrencyAccounting { get; set; }

        // trạng thái sử dụng
        public bool? UsingStatus { get; set; }

        // tên trạng thái sử dụng
        public string UsingStatusName { get; set; }
    }
}
