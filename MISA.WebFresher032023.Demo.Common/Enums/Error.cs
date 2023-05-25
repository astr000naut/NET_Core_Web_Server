using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WebFresher032023.Demo.Common.Enums
{
    public static class Error
    {
        // 500 - Server Error
        public const int ServerFailed = 500;
        public const string ServerFailedMessage = "Server đã xảy ra lỗi không xác định";

        public const int DbConnectFail = 506;
        public const string DbConnectFailMessage = "Xảy ra lỗi khi kết nối đến cơ sở dữ liệu";
        public const int DbQueryFail = 507;
        public const string DbQueryFailMsg = "Xảy ra lỗi khi truy vấn cơ sở dữ liệu";

        // 400 - BadInput
        public const int BadInput = 400;
        public const string IdListOversizeMsg = "Kích thước mảng ID quá lớn";

        //409 - Conflict
        public const int ConflictCode = 409;
        public const string InvalidDepartmentId = "Sai thông tin ID của đơn vị, vui lòng kiểm tra lại";
        public const string EmployeeCodeHasExist = "Mã nhân viên đã tồn tại, vui lòng kiểm tra lại";
        public const string DepartmentCodeHasExist = "Mã nhân đơn vị đã tồn tại, vui lòng kiểm tra lại";
        public const string InvalidEmployeeId = "Không tồn tại nhân viên với ID này";


        // 404 - Not Found
        public const int NotFound = 404;
        public const string NotFoundEmployeeMsg = "Không tìm thấy nhân viên";
        public const string NotFoundDepartmentMsg = "Không tìm thấy đơn vị"; 

    }
}
