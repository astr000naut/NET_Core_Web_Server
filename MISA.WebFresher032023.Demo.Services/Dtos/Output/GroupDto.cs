﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WebFresher032023.Demo.BusinessLayer.Dtos.Output
{
    public class GroupDto
    {
        public Guid GroupId { get; set; }

        public string GroupCode { get; set; }

        public string GroupName { get; set; }

        public bool IsParent { get; set; }

        public Guid? ParentId { get; set; }

        public int Grade { get; set; }

        public string MCodeId { get; set; }
    }
}
