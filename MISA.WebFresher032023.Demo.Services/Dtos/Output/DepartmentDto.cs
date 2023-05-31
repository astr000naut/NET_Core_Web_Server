using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WebFresher032023.Demo.BusinessLayer.Dtos.Output
{
    // Thông tin Department trả về cho client
    public class DepartmentDto
    {
        // ID  đơn vị char(36)
        public Guid DepartmentId { get; set; }
        // Mã đơn vị varchar(50)
        public string DepartmentCode { get; set; }
        // Tên đơn vị varchar(255)
        public string DepartmentName { get; set; }
    }
}
