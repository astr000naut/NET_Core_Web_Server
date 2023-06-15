using MISA.WebFresher032023.Demo.Common.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WebFresher032023.Demo.Common.Exceptions
{
    public class NotFoundException : BaseException
    {
        // Exception biểu thị không tìm thấy một đối tượng
        public NotFoundException(int errorCode, string message, string userMessage) : base(errorCode, message, userMessage) { }
    }
}
