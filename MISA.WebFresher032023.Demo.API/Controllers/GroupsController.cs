using Microsoft.AspNetCore.Mvc;
using MISA.WebFresher032023.Demo.BusinessLayer.Dtos.Input;
using MISA.WebFresher032023.Demo.BusinessLayer.Dtos.Input.Group;
using MISA.WebFresher032023.Demo.BusinessLayer.Dtos.Output;
using MISA.WebFresher032023.Demo.BusinessLayer.Services;
using MISA.WebFresher032023.Demo.BusinessLayer.Services.GroupSvc;

namespace MISA.WebFresher032023.Demo.API.Controllers
{
    [Route("api/v1/[controller]")]
    public class GroupsController : BaseController<GroupDto, GroupCreateDto, GroupUpdateDto>
    {
        public GroupsController(IGroupService groupService) : base(groupService) { }
    }
}
