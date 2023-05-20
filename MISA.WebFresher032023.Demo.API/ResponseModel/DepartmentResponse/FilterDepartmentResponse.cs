using MISA.WebFresher032023.Demo.BusinessLayer.Dtos.Output;
using MISA.WebFresher032023.Demo.ResponseModel;

namespace MISA.WebFresher032023.Demo.API.ResponseModel.DepartmentResponse
{
    public class FilterDepartmentResponse : BaseResponse
    {
        public FilteredListDto<DepartmentDto> Data { get; set; }
    }
}
