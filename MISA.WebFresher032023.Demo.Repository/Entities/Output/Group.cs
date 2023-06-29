using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WebFresher032023.Demo.DataLayer.Entities.Output
{
    public class Group : BaseOutputEntity
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
