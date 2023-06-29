using MISA.WebFresher032023.Demo.BusinessLayer.Dtos.Input.Group;
using MISA.WebFresher032023.Demo.BusinessLayer.Dtos.Output;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WebFresher032023.Demo.BusinessLayer.Services.GroupSvc
{
    public interface IGroupService:IBaseService<GroupDto, GroupCreateDto, GroupUpdateDto>
    {
    }
}
