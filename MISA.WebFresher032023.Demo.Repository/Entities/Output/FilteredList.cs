using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WebFresher032023.Demo.DataLayer.Entities.Output
{
    // Filtered Lít output Entity
    public class FilteredList<TEntity>
    {
        // Tổng số bản ghi tìm được
        public int TotalRecord { get; set; }
        // Danh sách dữ liệu nhận được
        public IEnumerable<TEntity?> ListData { get; set; }
    }
}
