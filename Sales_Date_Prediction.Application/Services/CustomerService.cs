using Sales_Date_Prediction.Domain.Entities;
using Sales_Date_Prediction.Domain.Interfaces;

namespace Sales_Date_Prediction.Application.Services
{
    public class CustomerService : ReadService<Customer>
    {
        protected override string SchemaName => "Sales";
        protected override string StoredProcName => "spCustomerDatePrediction";

        public CustomerService(IStoredProcedure<Customer> stored) : base(stored) { }
    }
}
