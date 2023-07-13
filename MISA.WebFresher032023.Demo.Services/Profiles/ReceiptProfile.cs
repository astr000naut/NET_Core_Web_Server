using AutoMapper;
using MISA.WebFresher032023.Demo.BusinessLayer.Dtos.Input;
using MISA.WebFresher032023.Demo.BusinessLayer.Dtos.Output;
using MISA.WebFresher032023.Demo.DataLayer.Entities.Input;
using MISA.WebFresher032023.Demo.DataLayer.Entities.Output;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WebFresher032023.Demo.BusinessLayer.Profiles
{
    public class ReceiptProfile : Profile
    {
        public ReceiptProfile() {

            CreateMap<Receipt, ReceiptDto>();

            CreateMap<ReceiptInputDto, ReceiptInput>();

            CreateMap<ReceiptDetail, ReceiptDetailDto>();

            CreateMap<ReceiptDetailInputDto, ReceiptDetailInput>();
        }
    }
}
