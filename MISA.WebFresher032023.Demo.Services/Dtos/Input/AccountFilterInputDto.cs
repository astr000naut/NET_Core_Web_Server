using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WebFresher032023.Demo.BusinessLayer.Dtos.Input
{
    // DTO filter danh sách tài khoản
    public class AccountFilterInputDto
    {
        // Bỏ qua bao nhiêu bản ghi
        public int Skip { get; set; }

        // Lấy bao nhiêu bản ghi
        public int? Take { get; set; }

        // String dùng để filter bản ghi
        [StringLength(100)]
        public string? KeySearch { get; set; }

        // danh sách id của các tài khoản tổng hợp
        [StringLength(500)]
        public string? ParentIdList { get; set; }

        // bậc
        public int? Grade { get; set; }
    }
}
