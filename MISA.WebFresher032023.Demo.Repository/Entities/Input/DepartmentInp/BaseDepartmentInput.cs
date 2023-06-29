using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WebFresher032023.Demo.DataLayer.Entities.Input
{
    // Base Input của Proc thêm hoặc cập nhật thông tin đơn vị
    public abstract class BaseDepartmentInput
    {
        // ID của đơn vị
        public Guid DepartmentId { get; set; }
        // Tên đơn vị
        public string DepartmentName { get; set; }
    }
}
