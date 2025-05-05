using Sales_Date_Prediction.Domain.Entities;
using Sales_Date_Prediction.Domain.Interfaces;

namespace Sales_Date_Prediction.Application.Services
{
    public class EmployeesService : ReadService<Employee>
    {
        protected override string SchemaName => "HR";
        protected override string StoredProcName => "spGetAllEmployees";

        public EmployeesService(IStoredProcedure<Employee> stored) : base(stored) { }
    }
}
