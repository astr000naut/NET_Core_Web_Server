using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using MISA.WebFresher032023.Demo.BusinessLayer.Dtos.Input;
using MISA.WebFresher032023.Demo.BusinessLayer.Dtos.Output;
using MISA.WebFresher032023.Demo.DataLayer.Entities.Input;
using MISA.WebFresher032023.Demo.DataLayer.Entities.Output;

namespace MISA.WebFresher032023.Demo.BusinessLayer.Profiles
{
    public class DepartmentProfile : Profile
    {
       public DepartmentProfile()
        {
            // Map từ Department sang DepartmentDto
            CreateMap<Department, DepartmentDto>();

            // Map từ DepartmentCreateDto sang DepartmentCreate
            CreateMap<DepartmentInputDto, DepartmentInput>();

        }

    }
}
