using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WebFresher032023.Demo.Common.Exceptions
{
    public class BadInputException : BaseException
    {
        /// <summary>
        /// Exception thể hiện các tham số của API gửi lên không hợp lệ
        /// </summary>
        /// <param name="errorCode">Mã lỗi</param>
        /// <param name="message">Dev message</param>
        /// <param name="userMessage">User message</param>
        public BadInputException(int errorCode, string message, string userMessage) : base(errorCode, message, userMessage) { }
    }
}
