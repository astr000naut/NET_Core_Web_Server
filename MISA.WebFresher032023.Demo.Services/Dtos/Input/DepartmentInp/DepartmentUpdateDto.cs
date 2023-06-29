using MISA.WebFresher032023.Demo.Common.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WebFresher032023.Demo.BusinessLayer.Dtos.Input
{ 
    // DTO cập nhật thông tin Department
    public class DepartmentUpdateDto : BaseDepartmentInputDto
    {
        // Mã đơn vị
        [StringLength(50)]
        public string? DepartmentCode { get; set; }
    }
}
