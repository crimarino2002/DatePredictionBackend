using Sales_Date_Prediction.Domain.Interfaces;
using Sales_Date_Prediction.Domain.Entities;

namespace Sales_Date_Prediction.API.Controllers
{
    public class EmployeesController : ReadController<Employee>
    {
        public EmployeesController(IReadService<Employee> service) : base(service) { }
    }
}