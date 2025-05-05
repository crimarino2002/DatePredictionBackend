using Sales_Date_Prediction.Domain.Entities;
using Sales_Date_Prediction.Domain.Interfaces;

namespace Sales_Date_Prediction.Application.Services
{
    public class OrdersService : WriteService<Order>
    {
        protected override string SchemaName => "Sales";
        protected override string IdParam => "@Id";

        protected override string SpGetDtoSimple => "spGetOrdersById";
        protected override string SpCreateOrder => "spCreateOrder";
        protected override string SpGetDtoPagedAllByParam => "spGetOrdersByCustomer";

        public OrdersService(IStoredProcedure<Order> sp) : base(sp) { }
    }
}
