using MISA.WebFresher032023.Demo.DataLayer.Entities.Output;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WebFresher032023.Demo.DataLayer.Repositories.LocationRepo
{
    public interface ILocationRepository
    {
        Task<List<Province>> getProvinceList(int countryId);
        Task<List<District>> getDistrictList(int provinceId);
        Task<List<Ward>> getWardList(int districtId);
    }
}
