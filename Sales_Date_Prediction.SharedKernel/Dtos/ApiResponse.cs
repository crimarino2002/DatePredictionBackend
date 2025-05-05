using System.Net;

namespace Sales_Date_Prediction.SharedKernel.Dtos
{
    public class ApiResponse<T>
    {
        public string Status { get; set; } = ((int)HttpStatusCode.OK).ToString();
        public string Message { get; set; } = HttpStatusCode.OK.ToString();

        public T Data { get; set; } = default!;

        public ResponseMetadata? Metadata { get; set; }
    }
}
