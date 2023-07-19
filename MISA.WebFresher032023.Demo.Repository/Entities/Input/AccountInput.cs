using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WebFresher032023.Demo.DataLayer.Entities.Input
{
    public class AccountInput
    {
        public Guid AccountId { get; set; }
        public string AccountNumber { get; set; }
        public string AccountNameVi { get; set; }
        public string AccountNameEn { get; set; }
        public Guid? ParentId { get; set; }
        public string? ParentNumber { get; set; }
        public int CategoryKind { get; set; }
        public string CategoryKindName { get; set; }
        public string Description { get; set; }
        public bool? DetailByBankAccount { get; set; }
        public bool? DetailByAccountObject { get; set; }
        public int? DetailByAccountObjectKind { get; set; }
        public DateTime CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public string? ModifiedBy { get; set; }
        public bool? ForeignCurrencyAccounting { get; set; }
        public bool? UsingStatus { get; set; }
        public string UsingStatusName { get; set; }
    }
}
