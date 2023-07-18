using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WebFresher032023.Demo.DataLayer.Entities.Input
{
    public class ReceiptDetailInput
    {
        public Guid ReceiptDetailId { get; set; }
        public Guid ReceiptId { get; set; }
        public string Description { get; set; }
        public Guid DebitAccountId { get; set; }
        public string DebitAccountNumber { get; set; }
        public Guid CreditAccountId { get; set; }
        public string CreditAccountNumber { get; set; }
        public string CustomerCode { get; set; }
        public string CustomerName { get; set; }
        public long Amount { get; set; }
        public DateTime CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public DateTime ModifiedDate { get; set; }
        public string ModifiedBy { get; set; }
    }
}
