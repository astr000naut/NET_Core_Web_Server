using MISA.WebFresher032023.Demo.BusinessLayer.DepartmentService.Dto.Output;

namespace MISA.WebFresher032023.Demo.ResponseModel.DepartmentResponse
{
    public class GetDepartmentListResponse : BaseResponse
    {
        public IEnumerable<DepartmentDto?> Data { get; set; }
    }
}
