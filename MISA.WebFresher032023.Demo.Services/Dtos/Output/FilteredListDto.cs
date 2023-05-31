using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WebFresher032023.Demo.BusinessLayer.Dtos.Output
{
    // Kết quả danh sách filter trả về cho client
    public class FilteredListDto<EntityDto>
    {
        // Tổng số bản ghi tìm thấy
        public int TotalRecord { get; set; }

        // Danh sách bản ghi trả về
        public List<EntityDto?> FilteredList { get; set; }
    }
}
