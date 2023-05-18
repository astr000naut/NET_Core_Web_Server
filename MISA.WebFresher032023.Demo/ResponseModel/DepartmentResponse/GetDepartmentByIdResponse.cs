using MISA.WebFresher032023.Demo.Entities;

namespace MISA.WebFresher032023.Demo.ResponseModel.DepartmentResponse
{
    public class GetDepartmentByIdResponse : BaseResponse
    {
        public Department? Department { get; set; }
    }
}
