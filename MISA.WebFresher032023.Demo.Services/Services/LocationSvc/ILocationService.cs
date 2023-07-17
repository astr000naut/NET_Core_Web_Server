using MISA.WebFresher032023.Demo.BusinessLayer.Dtos.Output;
using MISA.WebFresher032023.Demo.DataLayer.Entities.Output;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WebFresher032023.Demo.BusinessLayer.Services.LocationSvc
{
    public interface ILocationService
    {
        Task<List<ProvinceDto>> getProvinceList(int countryId);
        Task<List<DistrictDto>> getDistrictList(int provinceId);
        Task<List<WardDto>> getWardList(int districtId);
    }
}
