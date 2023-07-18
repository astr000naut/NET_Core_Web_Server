using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WebFresher032023.Demo.DataLayer.Entities.Output
{
    public class Receipt : BaseOutputEntity
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
        public int? DocumentIncluded { get; set; }
        public DateTime? PostedDate { get; set; }
        public DateTime? ReceiptDate { get; set; }
        public string ReceiptNo { get; set; }
        public long TotalAmount { get; set; }
        public bool LedgerStatus { get; set; }
    }
}
