using AutoMapper;
using MISA.WebFresher032023.Demo.BusinessLayer.Dtos.Output;
using MISA.WebFresher032023.Demo.DataLayer;
using MISA.WebFresher032023.Demo.DataLayer.Entities.Output;
using MISA.WebFresher032023.Demo.DataLayer.Repositories.LocationRepo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WebFresher032023.Demo.BusinessLayer.Services.LocationSvc
{
    public class LocationService : ILocationService
    {
        private IUnitOfWork _unitOfWork;
        private ILocationRepository _locationRepository;
        private IMapper _mapper;

        public LocationService(ILocationRepository locationRepository, IUnitOfWork unitOfWork, IMapper mapper) 
        {  
            _unitOfWork = unitOfWork; 
            _locationRepository = locationRepository;
            _mapper = mapper;
        }

        public async Task<List<DistrictDto>> getDistrictList(int provinceId)
        {
            var mKey = _unitOfWork.GetManipulationKey();
            try
            {
                _unitOfWork.SetManipulationKey(mKey + 1);
                await _unitOfWork.OpenAsync(mKey);
                var districtList = await _locationRepository.getDistrictList(provinceId);
                var districtListDto = new List<DistrictDto>();
                foreach (var district in districtList)
                {
                    var districtDto = _mapper.Map<DistrictDto>(district);
                    districtListDto.Add(districtDto);
                }
                return districtListDto;
            }
            finally
            {
                await _unitOfWork.CloseAsync(mKey);
            }
        }

        public async Task<List<ProvinceDto>> getProvinceList(int countryId)
        {
            var mKey = _unitOfWork.GetManipulationKey();
            try
            {
                _unitOfWork.SetManipulationKey(mKey + 1);
                await _unitOfWork.OpenAsync(mKey);
                var provinceList = await _locationRepository.getProvinceList(countryId);
                var provinceListDto = new List<ProvinceDto>();
                foreach (var province in provinceList)
                {
                    var provinceDto = _mapper.Map<ProvinceDto>(province);
                    provinceListDto.Add(provinceDto);
                }
                return provinceListDto;
            }
            finally
            {
                await _unitOfWork.CloseAsync(mKey);
            }
        }

        public async Task<List<WardDto>> getWardList(int districtId)
        {
            var mKey = _unitOfWork.GetManipulationKey();
            try
            {
                _unitOfWork.SetManipulationKey(mKey);
                await _unitOfWork.OpenAsync(mKey);
                var wardList = await _locationRepository.getWardList(districtId);
                var wardListDto = new List<WardDto>();
                foreach (var ward in wardList)
                {
                    var wardDto = _mapper.Map<WardDto>(ward);
                    wardListDto.Add(wardDto);
                }
                return wardListDto;
            }
            finally
            {
                await _unitOfWork.CloseAsync(mKey);
            }
        }
    }
}
