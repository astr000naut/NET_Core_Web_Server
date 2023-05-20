

using MISA.WebFresher032023.Demo.BusinessLayer.Dtos.Output;

namespace MISA.WebFresher032023.Demo.ResponseModel.EmployeeResponse
{
    public class FilterEmployeeResponse : BaseResponse
    {   
        public FilteredListDto<EmployeeDto> Data { get; set; }   
    }
}
