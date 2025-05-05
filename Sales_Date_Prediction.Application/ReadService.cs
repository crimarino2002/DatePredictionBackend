using Sales_Date_Prediction.Domain.Interfaces;
using Sales_Date_Prediction.SharedKernel.Dtos;

namespace Sales_Date_Prediction.Application
{
    public abstract class ReadService<TEntity> : IReadService<TEntity> where TEntity : DtoBase
    {
        private readonly IStoredProcedure<TEntity> _sp;
        
        protected abstract string SchemaName { get; }
        protected abstract string StoredProcName { get; }
        protected ReadService(IStoredProcedure<TEntity> sp) => _sp = sp;

        public async Task<(IEnumerable<TEntity> Items, int Total)> GetPagedAllAsync(int page, int pageSize, string filter)
        {
            var qualified = Qualify(SchemaName, StoredProcName);
            var all = (await _sp.ExecAsync(qualified)).ToList();

            if (!string.IsNullOrWhiteSpace(filter))
            {
                all = all
                    .Where(item =>
                        item.GetType()
                            .GetProperty("Name")!
                            .GetValue(item, null) is string name
                            && name.Contains(filter, StringComparison.OrdinalIgnoreCase)
                    ).ToList();
            }

            var skip = (page - 1) * pageSize;
            var paged = all.Skip(skip).Take(pageSize);
            var total = all.Count;

            return (paged, total);
        }

        private static string Qualify(string schema, string storedProc)
        {
            return $"[{schema}].[{storedProc}]";
        }
    }
}
