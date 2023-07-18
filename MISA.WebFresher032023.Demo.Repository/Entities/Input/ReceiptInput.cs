using MISA.WebFresher032023.Demo.DataLayer.Entities.Output;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WebFresher032023.Demo.DataLayer.Entities.Input
{
    public class ReceiptInput
    {
        public Guid ReceiptId { get; set; }
        public Guid? CustomerId { get; set; }
        public string? CustomerCode { get; set; }
        public string? CustomerName { get; set; }
        public string? ContactName { get; set; }
        public string? CustomerAddress { get; set; }
        public Guid? EmployeeId { get; set; }
        public string? EmployeeName { get; set; }
        public string? Reason { get; set; }
        public int? documentIncluded { get; set; }
        public DateTime? PostedDate { get; set; }
        public DateTime? ReceiptDate { get; set; }
        public string ReceiptNo { get; set; }
        public long TotalAmount { get; set; }

        public DateTime CreatedDate { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public string? ModifiedBy { get; set; }
        public bool LedgerStatus { get; set; }
    }
}
