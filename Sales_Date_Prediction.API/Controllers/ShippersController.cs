using Sales_Date_Prediction.Domain.Interfaces;
using Sales_Date_Prediction.Domain.Entities;

namespace Sales_Date_Prediction.API.Controllers
{
    public class ShippersController : ReadController<Shipper>
    {
        public ShippersController(IReadService<Shipper> service) : base(service) { }
    }
}