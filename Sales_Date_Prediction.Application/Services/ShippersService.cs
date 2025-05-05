using Sales_Date_Prediction.Domain.Entities;
using Sales_Date_Prediction.Domain.Interfaces;

namespace Sales_Date_Prediction.Application.Services
{
    public class ShippersService : ReadService<Shipper>
    {
        protected override string SchemaName => "Sales";
        protected override string StoredProcName => "spGetAllShippers";

        public ShippersService(IStoredProcedure<Shipper> stored) : base(stored) { }
    }
}
