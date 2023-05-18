namespace MISA.WebFresher032023.Demo.ResponseModel
{
    public class BaseResponse
    {
        public bool Success { get; set; }
        public string? ErrorCode { get; set; }
        public string? Message { get; set; }

        public BaseResponse()
        {
            Success = true;
            ErrorCode = null;
            Message = "";
        }
    }
}
