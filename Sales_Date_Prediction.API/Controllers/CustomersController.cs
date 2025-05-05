using Sales_Date_Prediction.Domain.Interfaces;
using Sales_Date_Prediction.Domain.Entities;

namespace Sales_Date_Prediction.API.Controllers
{
    public class CustomersController : ReadController<Customer>
    {
        public CustomersController(IReadService<Customer> service) : base(service) { }
    }
}