﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WebFresher032023.Demo.DataLayer.Entities.Input
{
    public abstract class BaseDepartmentInput 
    {
        // ID của đơn vị
        public Guid DepartmentId { get; set; }
        // Tên đơn vị
        public string DepartmentName { get; set; }

        // Ngày cập nhật cuối cùng
        public DateTime ModifiedDate { get; set; }
        // Cập nhật lần cuối bởi
        public string ModifiedBy { get; set; }
    }
}