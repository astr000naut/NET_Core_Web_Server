using MISA.WebFresher032023.Demo.Entities;

namespace MISA.WebFresher032023.Demo.ResponseModel.EmployeeResponse
{
    public class GetEmployeeByIdResponse : BaseResponse
    {
        public Employee? Employee { get; set; }
    }
}
