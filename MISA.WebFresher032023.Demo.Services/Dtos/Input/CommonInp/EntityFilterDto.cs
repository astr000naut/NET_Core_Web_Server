using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WebFresher032023.Demo.BusinessLayer.Dtos.Input
{
    // query params DTO để filter Department hoặc Employee
    public class EntityFilterDto
    {
        // Bỏ qua bao nhiêu bản ghi
        public int Skip { get; set; }

        // Lấy bao nhiêu bản ghi
        public int? Take { get; set; }

        // String dùng để filter bản ghi
        public string? KeySearch { get; set; }
    }
}
