using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WebFresher032023.Demo.DataLayer.Entities.Output
{
    // Department Output Entity
    public class Department : BaseOutputEntity
    {
        // ID đơn vị
        public Guid DepartmentId { get; set; }
        // Mã đơn vị
        public string DepartmentCode { get; set; }
        // Tên đơn vị
        public string DepartmentName { get; set; }
    }
}
