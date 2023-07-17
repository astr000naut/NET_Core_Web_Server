using Microsoft.AspNetCore.Mvc;
using MISA.WebFresher032023.Demo.BusinessLayer.Services.LocationSvc;
using MISA.WebFresher032023.Demo.Common.Enums;
using MISA.WebFresher032023.Demo.Common.Exceptions;

namespace MISA.WebFresher032023.Demo.API.Controllers
{
    [Route("api/v1/[controller]")]
    public class LocationsController : ControllerBase
    {
        private ILocationService _locationService;

        public LocationsController(ILocationService locationService)
        {
            _locationService = locationService;
        }

        [HttpGet("country/{id}")]
        public async Task<IActionResult> GetProvinceList(int id)
        {
            var result = await _locationService.getProvinceList(id);
            return (IActionResult)Ok(result);
        }

        [HttpGet("province/{id}")]
        public async Task<IActionResult> GetDistrictList(int id)
        {
            var result = await _locationService.getDistrictList(id);
            return (IActionResult)Ok(result);
        }

        [HttpGet("district/{id}")]
        public async Task<IActionResult> GetWardList(int id)
        {
            var result = await _locationService.getWardList(id);
            return (IActionResult)Ok(result);
        }
    }
}
