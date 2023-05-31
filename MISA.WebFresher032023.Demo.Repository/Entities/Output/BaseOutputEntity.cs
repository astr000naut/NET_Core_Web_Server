using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WebFresher032023.Demo.DataLayer.Entities.Output
{
    // Base của các Output Entity
    public abstract class BaseOutputEntity
    {
        // Ngày tạo
        public DateTime CreatedDate { get; set; }
        // Tạo bởi
        public string CreatedBy { get; set; }

        // Ngày chỉnh sửa lần cuối
        public DateTime? ModifiedDate { get; set; }
        // Chỉnh sửa lần cuối bởi
        public string ModifiedBy { get; set; }

    }
}
