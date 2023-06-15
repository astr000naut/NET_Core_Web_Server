using MISA.WebFresher032023.Demo.Common.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WebFresher032023.Demo.Common.Exceptions
{
    public class InternalException : BaseException
    {
        // Exception biểu thị có lỗi xảy ra bên trong server
        public InternalException(int errorCode, string message, string userMessage) : base(errorCode, message, userMessage) { }
    }
}
