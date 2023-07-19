using System;
using System.Collections.Generic;
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
        public string AccountNumber { get; set; }

        // tên tài khoản 
        public string AccountNameVi { get; set; }

        // tên tài khoản tiếng Anh
        public string AccountNameEn { get; set; }

        //số tài khoản tổng hợp
        public string? ParentNumber { get; set; }
        public Guid? ParentId { get; set; }
        public int CategoryKind { get; set; }
        public string CategoryKindName { get; set; }
        public string Description { get; set; }
        public bool? DetailByBankAccount { get; set; }
        public bool? DetailByAccountObject { get; set; }
        public int? DetailByAccountObjectKind { get; set; }
        public bool? ForeignCurrencyAccounting { get; set; }
        public bool? UsingStatus { get; set; }
        public string UsingStatusName { get; set; }

    }
}
