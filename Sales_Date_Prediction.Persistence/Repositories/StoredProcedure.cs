using System.Data;
using System.Runtime.CompilerServices;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Sales_Date_Prediction.Domain.Context;
using Sales_Date_Prediction.Domain.Interfaces;
using Sales_Date_Prediction.SharedKernel.Dtos;

namespace Sales_Date_Prediction.Domain.Repositories
{
    public class StoredProcedure<TEntity> : IStoredProcedure<TEntity> where TEntity : DtoBase
    {
        private const string _exec = "EXEC";
        private readonly AppDbContext _db;

        public StoredProcedure(AppDbContext db) =>
            _db = db;

        public async Task<IEnumerable<TEntity>> ExecAsync(string storedProcName)
        {
            var sql = $"{_exec} {storedProcName}";

            return await _db.Set<TEntity>()
                            .FromSqlRaw(sql)
                            .AsNoTracking()
                            .ToListAsync();
        }

        public async Task<IEnumerable<TEntity>> ExecParamAsync(string storedProcName, SqlParameter param)
        {
            var sql = $"{_exec} {storedProcName} {param.ParameterName} = {param.Value}";

            return await _db.Set<TEntity>()
                .FromSqlRaw(sql)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<IEnumerable<TEntity>> ExecScalarAsync(string storedProcName, SqlParameter[] parameters)
        {
            var sql = BuildSqlCommand(storedProcName, parameters);

            return await _db.Set<TEntity>()
                            .FromSqlInterpolated(sql)
                            .AsNoTracking()
                            .ToListAsync();
        }

        private static FormattableString BuildSqlCommand(string storedProcName, SqlParameter[] parameters)
        {
            if (parameters == null || parameters.Length == 0)
                return FormattableStringFactory.Create($"{_exec} {storedProcName}");

            var assigns = parameters
                .Select((p, i) => $"{p.ParameterName} = {{{i}}}")
                .ToArray();

            var commandText = $"{_exec} {storedProcName} {string.Join(", ", assigns)}";
            var values = parameters.Select(p => p.Value ?? DBNull.Value).ToArray();

            return FormattableStringFactory.Create(commandText, values);
        }
    }
}
