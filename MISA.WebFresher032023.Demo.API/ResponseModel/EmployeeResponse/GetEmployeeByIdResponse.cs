
using MISA.WebFresher032023.Demo.BusinessLayer.Dtos.Output;

namespace MISA.WebFresher032023.Demo.ResponseModel.EmployeeResponse
{
    public class GetEmployeeByIdResponse : BaseResponse
    {
        public EmployeeDto? Employee { get; set; }
    }
}
