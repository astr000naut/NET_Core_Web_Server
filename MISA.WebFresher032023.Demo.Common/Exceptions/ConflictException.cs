using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WebFresher032023.Demo.Common.Exceptions
{
    public class ConflictException : BaseException
    {
        public ConflictException(int errorCode, string message, string userMessage) : base(errorCode, message, userMessage) { }
    }
}
