using MISA.WebFresher032023.Demo.Entities;

namespace MISA.WebFresher032023.Demo.ResponseModel.EmployeeResponse
{
    public class FilterEmployeeResponse : BaseResponse
    {
        public int TotalPage { get; set; }
        public int TotalRecord { get; set; }
        public int CurrentPage { get; set; }
        public int CurrentPageRecords { get; set; }
        public IEnumerable<Employee>? Data { get; set; }
    }
}
