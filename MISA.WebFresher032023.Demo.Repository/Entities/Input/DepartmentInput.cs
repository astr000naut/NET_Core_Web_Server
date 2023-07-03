using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WebFresher032023.Demo.DataLayer.Entities.Input
{
    // Base Input của Proc thêm hoặc cập nhật thông tin đơn vị
    public class DepartmentInput
    {
        // ID của đơn vị
        public Guid DepartmentId { get; set; }
        // Tên đơn vị
        public string DepartmentName { get; set; }

        // Mã đơn vị
        public string DepartmentCode { get; set; }

        // Ngày tạo
        public DateTime CreatedDate { get; set; }
        // Tạo bởi
        public string CreatedBy { get; set; }

        // Ngày cập nhật cuối cùng
        public DateTime ModifiedDate { get; set; }
        // Cập nhật lần cuối bởi
        public string ModifiedBy { get; set; }
    }
}
