using MISA.WebFresher032023.Demo.BusinessLayer.Dtos.Output;

namespace MISA.WebFresher032023.Demo.ResponseModel.DepartmentResponse
{
    public class GetDepartmentByIdResponse : BaseResponse
    {
        public DepartmentDto? Department { get; set; }
    }
}
