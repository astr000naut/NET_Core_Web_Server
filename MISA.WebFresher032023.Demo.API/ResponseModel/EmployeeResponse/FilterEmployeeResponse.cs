

using MISA.WebFresher032023.Demo.BusinessLayer.EmployeeService.Dto.Output;

namespace MISA.WebFresher032023.Demo.ResponseModel.EmployeeResponse
{
    public class FilterEmployeeResponse : BaseResponse
    {   
        public EmployeeFilteredListDto Data { get; set; }   
    }
}
