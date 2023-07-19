using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WebFresher032023.Demo.BusinessLayer.Dtos.Output
{
    public class GroupDto
    {
        // id của nhóm
        public Guid GroupId { get; set; }


        // mã nhóm
        public string GroupCode { get; set; }

        // tên nhóm
        public string GroupName { get; set; }


        // là cha
        public bool IsParent { get; set; }

        // id của nhóm cha
        public Guid? ParentId { get; set; }

        // bậc
        public int Grade { get; set; }

        // mCodeId
        public string MCodeId { get; set; }
    }
}
