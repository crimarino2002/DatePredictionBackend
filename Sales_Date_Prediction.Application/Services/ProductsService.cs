using Sales_Date_Prediction.Domain.Entities;
using Sales_Date_Prediction.Domain.Interfaces;

namespace Sales_Date_Prediction.Application.Services
{
    public class ProductsService : ReadService<Product>
    {
        protected override string SchemaName => "Production";
        protected override string StoredProcName => "spGetAllProducts";

        public ProductsService(IStoredProcedure<Product> stored) : base(stored) { }
    }
}
