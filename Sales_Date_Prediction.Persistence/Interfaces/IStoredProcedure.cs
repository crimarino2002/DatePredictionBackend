using Microsoft.Data.SqlClient;
using Sales_Date_Prediction.SharedKernel.Dtos;

namespace Sales_Date_Prediction.Domain.Interfaces
{
    public interface IStoredProcedure<TEntity> where TEntity : DtoBase
    {
        Task<IEnumerable<TEntity>> ExecAsync(string storedProcName);
        Task<IEnumerable<TEntity>> ExecParamAsync(string storedProcName, SqlParameter param);
        Task<IEnumerable<TEntity>> ExecScalarAsync(string storedProcName, SqlParameter[] parameters);
    }
}
