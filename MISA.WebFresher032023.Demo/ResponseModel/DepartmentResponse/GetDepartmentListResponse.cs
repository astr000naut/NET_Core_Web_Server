using MISA.WebFresher032023.Demo.Entities;
namespace MISA.WebFresher032023.Demo.ResponseModel.DepartmentResponse
{
    public class GetDepartmentListResponse : BaseResponse
    {
        public IEnumerable<Department?> Data { get; set; }
    }
}
