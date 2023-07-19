using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WebFresher032023.Demo.BusinessLayer.Dtos.Output
{
    public class ReceiptDetailDto
    {
        // id của detail
        public Guid ReceiptDetailId { get; set; }

        // id phiếu thu
        public Guid ReceiptId { get; set; }

        // diễn giải
        public string Description { get; set; }

        // id tài khoản nợ
        public Guid DebitAccountId { get; set; }

        // tài khoản nợ
        public string DebitAccountNumber { get; set; }

        // id tài khoản có
        public Guid CreditAccountId { get; set; }

        // tài khoản có
        public string CreditAccountNumber { get; set; }

        // mã khách hàng
        public string CustomerCode { get; set; }

        // tên khách hàng
        public string CustomerName { get; set; }

        // số tiền
        public long Amount { get; set; }
    }
}
