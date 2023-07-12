using AutoMapper;
using MISA.WebFresher032023.Demo.BusinessLayer.Dtos.Input;
using MISA.WebFresher032023.Demo.BusinessLayer.Dtos.Output;
using MISA.WebFresher032023.Demo.DataLayer;
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
    public class GroupService : BaseService<Group, GroupDto, GroupInput, GroupInputDto>, IGroupService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IGroupRepository _groupRepository;

        public GroupService(IGroupRepository groupRepository, IMapper mapper, IUnitOfWork unitOfWork) : base(groupRepository, mapper, unitOfWork)
        {
            _groupRepository = groupRepository;
            _unitOfWork = unitOfWork;
        }
    }
}
