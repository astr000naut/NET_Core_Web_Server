using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WebFresher032023.Demo.Common.Exceptions
{
    public abstract class BaseException : Exception
    {
        // Mã lỗi
        public int ErrorCode { get; set; }
        // Message hiển thị ra cho người dùng
        public string? UserMessage { get; set; }

        // Lớp Exception cơ sở
        public BaseException(int errorCode, string message, string userMessage) : base(message) { 
            this.ErrorCode = errorCode;
            this.UserMessage = userMessage;
        }
    }
}
