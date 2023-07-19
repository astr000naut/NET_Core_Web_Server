using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WebFresher032023.Demo.BusinessLayer.Dtos.Input
{
    public class AccountChangeUsingStatusDto
    {
        //thay đổi những tài khoản bắt đầu bằng mCodeID
        [StringLength(255)]
        public string MCodeId { get; set; }

        // trạng thái sử dụng true - false
        public bool UsingStatus { get; set; }
    }
}
