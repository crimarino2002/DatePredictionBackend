using Sales_Date_Prediction.SharedKernel.Dtos;

namespace Sales_Date_Prediction.Domain.Entities
{
    public class Order : DtoBase
    {
        public int CustId { get; set; }
        public int EmpId { get; set; }
        public int ShipperId { get; set; }
        public int ProductId { get; set; }
        public DateTime? OrderDate { get; set; }
        public DateTime? RequiredDate { get; set; }
        public DateTime? ShippedDate { get; set; }
        public string? ShipName { get; set; }
        public string? ShipAddress { get; set; }
        public string? ShipCity { get; set; }
        public decimal Freight { get; set; }
        public string? ShipCountry { get; set; }
        public decimal UnitPrice { get; set; }
        public short Qty { get; set; }
        public decimal Discount { get; set; }
    }
}
