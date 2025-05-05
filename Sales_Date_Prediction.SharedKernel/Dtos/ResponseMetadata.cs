namespace Sales_Date_Prediction.SharedKernel.Dtos
{
    public class ResponseMetadata
    {
        public ResponseMetadata() { }

        public ResponseMetadata(string traceId, DateTime timestamp)
        {
            TraceId = traceId;
            Timestamp = timestamp;
        }

        public int? TotalItems { get; set; }
        public int? Page { get; set; }
        public int? PageSize { get; set; }
        public int? TotalPages => TotalPagesValue;

        public string? TraceId { get; set; }
        public DateTime? Timestamp { get; set; }

        private int TotalPagesValue =>
            (PageSize.HasValue && TotalItems.HasValue && PageSize > 0)
            ? (int)Math.Ceiling((double)TotalItems.Value / PageSize.Value) : 0;
    }
}
