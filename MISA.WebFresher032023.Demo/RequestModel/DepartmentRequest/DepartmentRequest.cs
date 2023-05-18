using System.Net;

namespace MISA.WebFresher032023.Demo.RequestModel.DepartmentRequest
{
    public class DepartmentRequest
    {
        public string departmentCode { get; set; }
        public string departmentName { get; set; }
        public DateTime createdDate { get; set; }
        public string createdBy { get; set; }
        public DateTime modifiedDate { get; set; }
        public string modifiedBy { get; set; }
        
        public void ValidateDataType()
        {
            if (departmentCode.Length == 0) { throw new Exception("Mã đơn vị không được để trống"); }
            if (departmentName.Length == 0) { throw new Exception("Tên đơn vị không được để trống"); }


            if (departmentCode.Length > 50) { throw new Exception("Giá trị Mã đơn vị quá dài"); }
            if (departmentName.Length > 255) { throw new Exception("Giá trị Tên đơn vị quá dài"); }
            if (createdBy.Length > 255) { throw new Exception("Giá trị Người tạo quá dài"); }
            if (modifiedBy?.Length > 255) { throw new Exception("Giá trị Người sửa quá dài"); }

        }
    }
}
