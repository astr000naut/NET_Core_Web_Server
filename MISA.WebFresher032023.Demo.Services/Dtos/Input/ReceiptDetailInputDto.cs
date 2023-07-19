using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WebFresher032023.Demo.BusinessLayer.Dtos.Input
{
    public class ReceiptDetailInputDto
    {
        // trạng thái thao tác 
        public string? Status { get; set; }

        // id của chi tiết phiếu thu
        public Guid? ReceiptDetailId { get; set; }

        // id phiếu thu
        public Guid? ReceiptId { get; set; }

        // diễn giải
        [StringLength(255)]
        public string? Description { get; set; }

        // id tài khoản nợ
        public Guid DebitAccountId { get; set; }

        // tài khoản nợ
        [StringLength(50)]
        public string DebitAccountNumber { get; set; }

        // id tài khoản có
        public Guid CreditAccountId { get; set; }

        // tài khoản có
        [StringLength(50)]
        public string CreditAccountNumber { get; set; }

        // mã khách hàng
        [StringLength(50)]
        public string? CustomerCode { get; set; }

        // tên khách hàng
        [StringLength(255)]
        public string? CustomerName { get; set; }

        // số tiền
        public long? Amount { get; set; }
    }
}
