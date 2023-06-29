using AutoMapper;
using MISA.WebFresher032023.Demo.BusinessLayer.Dtos.Input.Group;
using MISA.WebFresher032023.Demo.BusinessLayer.Dtos.Output;
using MISA.WebFresher032023.Demo.DataLayer.Entities.Input;
using MISA.WebFresher032023.Demo.DataLayer.Entities.Output;
using MISA.WebFresher032023.Demo.DataLayer.Repositories;
using MISA.WebFresher032023.Demo.DataLayer.Repositories.GroupRepo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WebFresher032023.Demo.BusinessLayer.Services.GroupSvc
{
    public class GroupService : BaseService<Group, GroupDto, GroupCreate, GroupCreateDto, GroupUpdate, GroupUpdateDto>, IGroupService
    {
        private readonly IGroupRepository _groupRepository;

        public GroupService(IGroupRepository groupRepository, IMapper mapper) : base(groupRepository, mapper)
        {
            _groupRepository = groupRepository;
        }
    }
}
