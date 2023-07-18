using MISA.WebFresher032023.Demo.BusinessLayer.Dtos.Output;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WebFresher032023.Demo.BusinessLayer.Dtos.Input
{
    public class ReceiptInputDto
    {
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
        public long? TotalAmount { get; set; }

        public bool LedgerStatus { get; set; }
        public List<ReceiptDetailInputDto> ReceiptDetailList { get; set; }
    }
}
