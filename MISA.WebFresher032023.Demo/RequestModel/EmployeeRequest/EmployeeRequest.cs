namespace MISA.WebFresher032023.Demo.RequestModel.EmployeeRequest
{
    public class EmployeeRequest
    {
        public string employeeCode { get; set; }
        public string employeeFullName { get; set; }
        public Guid departmentId { get; set; }
        public string? positionName { get; set; }
        public DateTime? dateOfBirth { get; set; }
        public int? gender { get; set; }
        public string? genderName { get; set; }
        public string? identityNumber { get; set; }
        public DateTime? identityDate { get; set; }
        public string? identityPlace { get; set; }
        public string? address { get; set; }
        public string? phoneNumber { get; set; }
        public string? landlineNumber { get; set; }
        public string? email { get; set; }
        public string? bankAccount { get; set; }
        public string? bankName { get; set; }
        public string? bankBranch { get; set; }

        public DateTime createdDate { get; set; }
        public string createdBy { get; set; }
        public DateTime? modifiedDate { get; set; }
        public string? modifiedBy { get; set;}

        public void ValidateDataType()
        {
            if (employeeCode.Length == 0) { throw new Exception("Mã nhân viên không được để trống"); }
            if (employeeFullName.Length == 0) { throw new Exception("Tên nhân viên không được để trống"); }


            if (employeeCode.Length > 50) { throw new Exception("Giá trị Mã nhân viên quá dài"); }
            if (employeeFullName.Length > 100) { throw new Exception("Giá trị Tên nhân viên quá dài"); }
            if (positionName?.Length > 255) { throw new Exception("Giá trị Vị trí quá dài"); }
            if (address?.Length > 255) { throw new Exception("Giá trị Địa chỉ quá dài"); }
            if (phoneNumber?.Length > 50) { throw new Exception("Giá trị Số điện thoại quá dài"); }
            if (landlineNumber?.Length > 50) { throw new Exception("Giá trị Số điện thoại cố dịnh quá dài"); }
            if (email?.Length > 50) { throw new Exception("Giá trị Email quá dài"); }
            if (bankAccount?.Length > 50) { throw new Exception("Giá trị Tài khoản ngân hàng quá dài"); }
            if (bankName?.Length > 255) { throw new Exception("Giá trị Tên ngân hàng quá dài"); }
            if (bankBranch?.Length > 255) { throw new Exception("Giá trị Chi nhánh ngân hàng quá dài"); }
            if (createdBy.Length > 255) { throw new Exception("Giá trị Người tạo quá dài"); }
            if (modifiedBy?.Length > 255) { throw new Exception("Giá trị Người sửa quá dài"); }

        }
    }
}
