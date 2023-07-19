using MISA.WebFresher032023.Demo.BusinessLayer.Dtos.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WebFresher032023.Demo.BusinessLayer.Dtos.Output
{
    public class ReceiptDto
    {
        // id phiếu thu
        public Guid ReceiptId { get; set; }

        // id khách hàng
        public Guid? CustomerId { get; set; }

        // mã khách hàng
        public string? CustomerCode { get; set; }

        // tên khách hàng
        public string? CustomerName { get; set; }

        // tên người liên hệ
        public string? ContactName { get; set; }

        // địa chỉ khách hàng
        public string? CustomerAddress { get; set; }

        // id nhân vien bán hàng
        public Guid? EmployeeId { get; set; }

        // tên nhân viên bán hàng
        public string? EmployeeName { get; set; }

        // lý do thu tiền
        public string? Reason { get; set; }

        // số tài liệu đính kèm
        public int? DocumentIncluded { get; set; }

        // ngày hạch toán
        public DateTime? PostedDate { get; set; }

        // ngày phiếu thu
        public DateTime? ReceiptDate { get; set; }

        // số phiếu thu
        public string ReceiptNo { get; set; }
        
        // tổng số tiền
        public long TotalAmount { get; set; }

        // trạng thái ghi sổ
        public bool LedgerStatus { get; set; }

        // danh sách detail
        public List<ReceiptDetailDto> ReceiptDetailList { get; set; }
    }
}
