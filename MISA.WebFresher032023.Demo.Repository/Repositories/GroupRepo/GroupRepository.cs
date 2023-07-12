using Microsoft.Extensions.Configuration;
using MISA.WebFresher032023.Demo.DataLayer.Entities.Input;
using MISA.WebFresher032023.Demo.DataLayer.Entities.Output;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WebFresher032023.Demo.DataLayer.Repositories.GroupRepo
{
    public class GroupRepository : BaseRepository<Group, GroupInput>, IGroupRepository
    { 
        public GroupRepository(IUnitOfWork unitOfWork) : base(unitOfWork) { }
    }
}
