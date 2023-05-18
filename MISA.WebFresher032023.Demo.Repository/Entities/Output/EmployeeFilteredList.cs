using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WebFresher032023.Demo.DataLayer.Entities.Output
{
    public class EmployeeFilteredList
    {
        public int TotalRecord { get; set; }
        public IEnumerable<Employee?> FilteredList { get; set; }
    }
}
