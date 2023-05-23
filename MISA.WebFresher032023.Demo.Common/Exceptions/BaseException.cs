using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WebFresher032023.Demo.Common.Exceptions
{
    public abstract class BaseException : Exception
    {
        public int ErrorCode { get; set; }
        public string? UserMessage { get; set; }

        public BaseException(int errorCode, string message, string userMessage) : base(message) { 
            this.ErrorCode = errorCode;
            this.UserMessage = userMessage;
        }
    }
}
