using MISA.WebFresher032023.Demo.Common.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace MISA.WebFresher032023.Demo.Common.Exceptions
{
    // Dữ liệu trả về cho người dùng khi gặp exception
    public class ExceptionResponse
    {
        public int ErrorCode { get; set; }
        public string? UserMessage { get; set; }
        public string? DevMessage { get; set; }
        public string? TraceId { get; set; }

        /// <summary>
        /// Override hàm ép kiểu sang String
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return JsonSerializer.Serialize(this);
        }

    }
}
