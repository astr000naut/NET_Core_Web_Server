using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WebFresher032023.Demo.BusinessLayer.Dtos.Input
{
    public class ReceiptDetailInputDto
    {
        public string status { get; set; }
        public Guid? receiptDetailId { get; set; }
        public Guid? receiptId { get; set; }
        public string? description { get; set; }
        public Guid debitAccountId { get; set; }
        public string debitAccountNumber { get; set; }
        public Guid creditAccountId { get; set; }
        public string creditAccountNumber { get; set; }
        public string? customerCode { get; set; }
        public string? customerName { get; set; }
        public long? amount { get; set; }
    }
}
