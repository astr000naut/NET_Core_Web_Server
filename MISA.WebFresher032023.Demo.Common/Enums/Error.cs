using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WebFresher032023.Demo.Common.Enums
{
    public static class Error
    {
        #region Server Error
        // 500 - Server Error
        public const int ServerFailed = 500;
        public const string ServerFailedMsg = "Server đã xảy ra lỗi, vui lòng liên hệ nhân viên hỗ trợ";

        public const int DbConnectFail = 506;
        public const string DbConnectFailMsg = "Xảy ra lỗi khi kết nối đến cơ sở dữ liệu";

        public const int DbQueryFail = 507;
        public const string DbQueryFailMsg = "Xảy ra lỗi khi truy vấn cơ sở dữ liệu";

        public const int ExportFail = 507;
        public const string ExportFailMsg = "Xảy ra lỗi khi xuất khẩu dữ liệu";
        #endregion

        #region BadInput
        // 400 - BadInput
        public const int BadInput = 400;
        public const string IdListOversizeMsg = "Kích thước mảng ID quá lớn";
        #endregion

        #region Conflict
        //409 - Conflict
        public const int ConflictCode = 409;
        public const string InvalidDepartmentIdMsg = "Sai thông tin ID của đơn vị, vui lòng kiểm tra lại";
        public const string EmployeeCodeHasExistMsg = "Mã nhân viên đã tồn tại, vui lòng kiểm tra lại";
        public const string CustomerCodeHasExistMsg = "Mã khách hàng đã tồn tại, vui lòng kiểm tra lại";
        public const string DepartmentCodeHasExistMsg = "Mã nhân đơn vị đã tồn tại, vui lòng kiểm tra lại";
        public const string InvalidEmployeeIdMsg = "Không tồn tại nhân viên với ID này";
        public const string InvalidCustomerIdMsg = "Không tồn tại khách hàng với ID này";
        public const string AccountDeleteConflictMsg = "Không thể xóa danh mục cha nếu chưa xóa danh mục con";

        #endregion

        #region Notfound
        // 404 - Not Found
        public const int NotFound = 404;
        public const string NotFoundEmployeeMsg = "Không tồn tại nhân viên này";
        public const string NotFoundDepartmentMsg = "Không tìm thấy đơn vị";
        #endregion
    }
}
