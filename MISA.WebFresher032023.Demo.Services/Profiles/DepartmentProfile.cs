using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using MISA.WebFresher032023.Demo.BusinessLayer.DepartmentService.Dto.FromClient;
using MISA.WebFresher032023.Demo.BusinessLayer.DepartmentService.Dto.Output;
using MISA.WebFresher032023.Demo.DataLayer.Entities.Input;
using MISA.WebFresher032023.Demo.DataLayer.Entities.Output;

namespace MISA.WebFresher032023.Demo.BusinessLayer.Profiles
{
    public class DepartmentProfile : Profile
    {
       public DepartmentProfile()
        {
            CreateMap<Department, DepartmentDto>();
            CreateMap<DepartmentCreateDto, DepartmentCreate>();
            CreateMap<DepartmentUpdateDto, DepartmentUpdate>();
        }

    }
}
