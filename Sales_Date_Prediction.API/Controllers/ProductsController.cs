using Sales_Date_Prediction.Domain.Interfaces;
using Sales_Date_Prediction.Domain.Entities;

namespace Sales_Date_Prediction.API.Controllers
{
    public class ProductController : ReadController<Product>
    {
        public ProductController(IReadService<Product> service) : base(service) {}
    }
}