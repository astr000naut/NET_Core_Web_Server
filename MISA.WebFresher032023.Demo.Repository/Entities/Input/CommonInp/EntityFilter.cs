using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WebFresher032023.Demo.DataLayer.Entities.Input
{
    // Input class của Proc filter nhân viên, filter đơn vị
    public class EntityFilter
    {
        // Bỏ qua bao nhiêu bản ghi
        public int Skip { get; set; }

        // Lấy bao nhiêu bản ghi
        public int? Take { get; set; }

        // String dùng để filter bản ghi
        public string? KeySearch { get; set; }

    }
}
