using Sales_Date_Prediction.SharedKernel.Dtos;

namespace Sales_Date_Prediction.Domain.Entities
{
    public class Customer : DtoBase
    {
        public string? Name { get; set; }
        public DateTime? LastOrderDate { get; set; }
        public DateTime? NextPredictedOrder { get; set; }
    }
}
