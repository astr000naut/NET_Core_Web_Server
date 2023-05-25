using MISA.WebFresher032023.Demo.Common.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WebFresher032023.Demo.DataLayer.Entities.Input
{
    public class EmployeeCreate : BaseEmployeeInput
    {
        // Ngày tạo
        public DateTime CreatedDate { get; set; }
        // Tạo bởi
        public string CreatedBy { get; set; }
    }
}
