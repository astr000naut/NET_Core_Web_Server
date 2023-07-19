using MISA.WebFresher032023.Demo.BusinessLayer.Dtos.Output;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WebFresher032023.Demo.BusinessLayer.Dtos.Input
{
    public class ReceiptInputDto
    {
        // id khách hàng
        public Guid? CustomerId { get; set; }

        //mã khách hàng
        [StringLength(50)]
        public string? CustomerCode { get; set; }

        // tên khách hàng
        [StringLength(100)]
        public string? CustomerName { get; set; }

        // tên người liên hệ
        [StringLength(100)]
        public string? ContactName { get; set; }

        // địa chỉ khách hàng
        [StringLength(255)]
        public string? CustomerAddress { get; set; }

        // id nhân viên bán hàng
        public Guid? EmployeeId { get; set; }

        // tên nhân viên bán hàng
        [StringLength(100)]
        public string? EmployeeName { get; set; }

        // lí do thu tiền
        [StringLength(255)]
        public string? Reason { get; set; }

        // số lượng tài liệu đính kèm
        public int? DocumentIncluded { get; set; }

        // ngày hạch toán
        public DateTime? PostedDate { get; set; }

        // ngày phiếu thu
        public DateTime? ReceiptDate { get; set; }

        // số phiếu thu
        [StringLength(50)]
        public string ReceiptNo { get; set; }

        // tổng số tiền
        public long? TotalAmount { get; set; }

        // trạng thái ghi sổ
        public bool LedgerStatus { get; set; }

        // danh sách các detail
        public List<ReceiptDetailInputDto> ReceiptDetailList { get; set; }
    }
}
