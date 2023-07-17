using Dapper;
using MISA.WebFresher032023.Demo.Common.Enums;
using MISA.WebFresher032023.Demo.Common.Exceptions;
using MISA.WebFresher032023.Demo.DataLayer.Entities.Input;
using MISA.WebFresher032023.Demo.DataLayer.Entities.Output;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WebFresher032023.Demo.DataLayer.Repositories.LocationRepo
{
    public class LocationRepository : ILocationRepository   
    {
        private IUnitOfWork _unitOfWork;
        public LocationRepository(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<List<District>> getDistrictList(int provinceId)
        {
            try
            {
                var parameter = new { ProvinceId = provinceId };
                var queryString = $"SELECT d.DistrictId, d.Name FROM district d WHERE d.ProvinceId=@ProvinceId;";

                var districtList = await _unitOfWork.Connection.QueryAsync<District>(queryString, parameter);
                return districtList.ToList();

            }
            catch (Exception ex)
            {
                throw new DbException(Error.DbQueryFail, ex.Message, Error.DbQueryFailMsg);
            }
        }

        public async Task<List<Province>> getProvinceList(int countryId)
        {
            try
            {
                var queryString = $"SELECT p.ProvinceId, p.Name FROM province p;";
                var proviceList = await _unitOfWork.Connection.QueryAsync<Province>(queryString);

                return proviceList.ToList();
            }
            catch (Exception ex)
            {

                throw new DbException(Error.DbQueryFail, ex.Message, Error.DbQueryFailMsg);
            }
        }

        public async Task<List<Ward>> getWardList(int districtId)
        {
            try
            {
                var parameter = new {DistrictId = districtId};
                var queryString = $"SELECT w.WardId, w.Name FROM ward w WHERE w.DistrictId=@DistrictId;";

                var wardList = await _unitOfWork.Connection.QueryAsync<Ward>(queryString, parameter);
                return wardList.ToList();

            }
            catch (Exception ex)
            {
                throw new DbException(Error.DbQueryFail, ex.Message, Error.DbQueryFailMsg);
            }
        }
    }
}
