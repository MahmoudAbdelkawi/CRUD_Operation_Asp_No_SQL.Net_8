using System.Net;

namespace Online_Survey.Global
{
    public class BaseResponse<T> 
    {
        public bool Succeeded { get; set; }
        public string Message { get; set; } = null!;
        public T Data { get; set; }
        public HttpStatusCode StatusCode { get; set; }
    }
}
