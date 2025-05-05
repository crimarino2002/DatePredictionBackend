namespace Sales_Date_Prediction.SharedKernel.Dtos
{
    public class PagedResponse<T> : ApiResponse<IEnumerable<T>>
    {
        public new ResponseMetadata Metadata { get; set; } = new ResponseMetadata();
    }
}
