using MISA.WebFresher032023.Demo.Entities;

namespace MISA.WebFresher032023.Demo.ResponseModel.EmployeeResponse
{
    public class GetEmployeeListResponse : BaseResponse
    {
        public IEnumerable<Employee?> Data { get; set; }
    }
}
