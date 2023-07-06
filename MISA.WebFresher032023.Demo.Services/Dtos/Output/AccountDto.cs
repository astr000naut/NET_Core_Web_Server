﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WebFresher032023.Demo.BusinessLayer.Dtos.Output
{
    public class AccountDto
    {
        public Guid AccountId { get; set; }
        public string AccountNumber { get; set; }
        public string AccountNameVi { get; set; }
        public string? AccountNameEn { get; set; }
        public bool IsParent { get; set; }
        public Guid? ParentId { get; set; }
        public string? ParentNumber { get; set; }
        public int CategoryKind { get; set; }
        public string? Description { get; set; }
        public bool? DetailByBankAccount { get; set; }
        public bool? DetailByAccountObject { get; set; }
        public int? DetailByAccountObjectKind { get; set; }
        public int Grade { get; set; }
        public string MCodeId { get; set; }
    }
}
