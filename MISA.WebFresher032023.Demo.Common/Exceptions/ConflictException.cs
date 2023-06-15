using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WebFresher032023.Demo.Common.Exceptions
{
    public class ConflictException : BaseException
    {
        // Exception thể hiện có mâu thuẫn giữa dữ liệu người dùng gửi lên và dữ liệu trong Database
        public ConflictException(int errorCode, string message, string userMessage) : base(errorCode, message, userMessage) { }
    }
}
