using MISA.WebFresher032023.Demo.DataLayer.Entities.Output;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WebFresher032023.Demo.BusinessLayer.EmployeeService.Dto.Output
{
    public class EmployeeFilteredListDto
    {
        public int TotalRecord { get; set; }
        public List<EmployeeDto?> FilteredList { get; set; }
    }
}
