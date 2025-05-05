using Sales_Date_Prediction.SharedKernel.Dtos;
namespace Sales_Date_Prediction.Domain.Interfaces
{
    public interface IWriteService<TEntity> where TEntity : DtoBase
    {
        Task<TEntity?> CreateAsync(TEntity dto);
        Task<(IEnumerable<TEntity> Items, int Total)> GetPagedAllByParamAsync(string paramValue, int page, int pageSize);
    }
}
