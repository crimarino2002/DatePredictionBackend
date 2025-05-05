using Microsoft.Data.SqlClient;
using Sales_Date_Prediction.Domain.Entities;
using Sales_Date_Prediction.Domain.Interfaces;
using Sales_Date_Prediction.SharedKernel.Dtos;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;

namespace Sales_Date_Prediction.Application
{
    public abstract class WriteService<TEntity> : IWriteService<TEntity> where TEntity : DtoBase
    {
        private readonly IStoredProcedure<TEntity> _sp;
        protected abstract string SchemaName { get; }
        protected abstract string IdParam { get; }

        protected abstract string SpGetDtoSimple { get; }
        protected abstract string SpCreateOrder { get; }
        protected abstract string SpGetDtoPagedAllByParam { get; }
        protected WriteService(IStoredProcedure<TEntity> sp) => _sp = sp;

        public async Task<(IEnumerable<TEntity> Items, int Total)> GetPagedAllByParamAsync(string paramValue, int page, int pageSize)
        {
            var param = new SqlParameter(IdParam, paramValue);
            var qualified = Qualify(SchemaName, SpGetDtoPagedAllByParam);
            var all = (await _sp.ExecParamAsync(qualified, param)).ToList();

            var skip = (page - 1) * pageSize;
            var paged = all.Skip(skip).Take(pageSize);
            var total = all.Count;

            return (paged, total);
        }

        public async Task<TEntity?> CreateAsync(TEntity dto)
        {
            var parameters = ToSqlParameters(dto);
            var qualified = Qualify(SchemaName, SpCreateOrder);
            var all = (await _sp.ExecScalarAsync(qualified, parameters)).ToList();
            return all.FirstOrDefault();
        }

        private static SqlParameter[] ToSqlParameters(object dto)
        {
            var list = new List<SqlParameter>();
            AddParametersRecursive(dto, prefix: null, list);
            return list
                .Where(p => !string.Equals(p.ParameterName.TrimStart('@'), "Id", StringComparison.OrdinalIgnoreCase))
                .ToArray();
        }

        private static void AddParametersRecursive(object? obj, string? prefix, List<SqlParameter> list)
        {
            if (obj == null) return;

            var type = obj.GetType();

            if (IsSimple(type))
            {
                var name = $"@{prefix}";
                var value = obj;
                list.Add(new SqlParameter(name, value ?? DBNull.Value));
                return;
            }

            var props = type.GetProperties(BindingFlags.Instance | BindingFlags.Public)
                .Where(p => p.CanRead);

            foreach (var prop in props)
            {
                var val = prop.GetValue(obj);
                var propName = prefix == null
                    ? prop.Name
                    : $"{prop.Name}";
                    AddParametersRecursive(val, propName, list);
            }
        }

        private static bool IsSimple(Type type)
        {
            return
                type.IsPrimitive
             || type.IsEnum
             || type == typeof(string)
             || type == typeof(decimal)
             || type == typeof(DateTime)
             || type == typeof(DateTimeOffset)
             || type == typeof(Guid)
             || type == typeof(bool)
             || Nullable.GetUnderlyingType(type) != null && IsSimple(Nullable.GetUnderlyingType(type)!);
        }

        private static string Qualify(string schema, string storedProc)
        {
            return $"[{schema}].[{storedProc}]";
        }
    }
}
