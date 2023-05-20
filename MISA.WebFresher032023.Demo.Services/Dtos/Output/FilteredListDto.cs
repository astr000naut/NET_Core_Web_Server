using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WebFresher032023.Demo.BusinessLayer.Dtos.Output
{
    public class FilteredListDto<EntityDto>
    {
        public int TotalRecord { get; set; }
        public List<EntityDto?> FilteredList { get; set; }
    }
}
