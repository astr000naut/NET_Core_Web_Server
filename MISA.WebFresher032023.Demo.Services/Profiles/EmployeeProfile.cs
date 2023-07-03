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
    public class EmployeeProfile : Profile
    {
        public EmployeeProfile()
        {
            // Map từ Employee sang EmployeeDto
            CreateMap<Employee, EmployeeDto>();

            // Map từ EmployeeUpdateDto sang EmployeeUpdate
            CreateMap<EmployeeInputDto, EmployeeInput>();

        }
    }
}
