using MISA.WebFresher032023.Demo.Common.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WebFresher032023.Demo.DataLayer.Entities.Input
{
    public class DepartmentUpdate : BaseDepartmentInput
    {
        // Mã đơn vị
        public string DepartmentCode { get; set; }
    }
}
