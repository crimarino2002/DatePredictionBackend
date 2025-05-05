using Sales_Date_Prediction.SharedKernel.Dtos;

namespace Sales_Date_Prediction.Domain.Interfaces
{
    public interface IReadService<TEntity> where TEntity : DtoBase
    {
        Task<(IEnumerable<TEntity> Items, int Total)> GetPagedAllAsync(int page, int pageSize, string filter);
    }
}
