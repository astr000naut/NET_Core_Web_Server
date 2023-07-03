using MISA.WebFresher032023.Demo.Common.Enum;
using MISA.WebFresher032023.Demo.Common.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WebFresher032023.Demo.BusinessLayer.Dtos.Input
{
    // Base DTO cho Department
    public class DepartmentInputDto
    {
        // Tên đơn vị
        [StringLength(255)]
        public string DepartmentName { get; set; }

        // Mã đơn vị
        [StringLength(50)]
        public string? DepartmentCode { get; set; }
    }
}
