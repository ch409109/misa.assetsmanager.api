using Dapper;
using Microsoft.Extensions.Configuration;
using Misa.AssetManagement.Core.Interfaces.Repositories;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Net.WebSockets;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Misa.AssetManagement.Infrastructure.Repositories
{
    public class BaseRepository<T> : IBaseRepository<T>, IDisposable where T : class
    {
        protected readonly string connectionString;
        protected IDbConnection dbConnection;

        public BaseRepository(IConfiguration configuration)
        {
            connectionString = configuration.GetConnectionString("DefaultConnection");
            dbConnection = new MySqlConnection(connectionString);
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            var tableAttr = typeof(T).GetCustomAttribute<TableAttribute>();
            var tableName = tableAttr != null ? tableAttr.Name : typeof(T).Name.ToLower();

            var sqlCommand = $"SELECT * FROM {tableName}";

            var data = await dbConnection.QueryAsync<T>(sqlCommand);

            return data;
        }

        public async Task<T?> GetByIdAsync(Guid id)
        {
            var tableAttr = typeof(T).GetCustomAttribute<TableAttribute>();
            var tableName = tableAttr != null ? tableAttr.Name : typeof(T).Name.ToLower();

            var sqlCommand = $"SELECT * FROM {tableName} WHERE {tableName}_id = @Id";

            var result = await dbConnection.QueryFirstOrDefaultAsync<T>(sqlCommand, new { Id = id });

            return result;
        }

        public async Task<T> CreateAsync(T entity)
        {
            var properties = typeof(T).GetProperties();
            var tableAttr = typeof(T).GetCustomAttribute<TableAttribute>();
            var tableName = tableAttr != null ? tableAttr.Name : typeof(T).Name.ToLower();
            var columns = "";
            var columnParams = "";
            var parameters = new DynamicParameters();
            foreach (var prop in properties)
            {
                var keyAttr = prop.GetCustomAttribute<KeyAttribute>();
                if (keyAttr != null)
                {
                    continue;
                }
                var columnAttr = prop.GetCustomAttribute<ColumnAttribute>();
                var columnName = columnAttr != null ? columnAttr.Name : prop.Name.ToLower();

                columns += $"{columnName},";
                columnParams += $"@{prop.Name},";
                parameters.Add($"@{prop.Name}", prop.GetValue(entity));
            }

            columns = columns.TrimEnd(',');
            columnParams = columnParams.TrimEnd(',');

            var sqlCommand = $"INSERT INTO {tableName} ({columns}) VALUES ({columnParams})";

            await dbConnection.ExecuteAsync(sqlCommand, parameters);

            return entity;
        }

        public Task<int> UpdateAsync(Guid id, T entity)
        {
            var properties = typeof(T).GetProperties();
            var tableAttr = typeof(T).GetCustomAttribute<TableAttribute>();
            var tableName = tableAttr != null ? tableAttr.Name : typeof(T).Name.ToLower();
            var setClause = "";
            var parameters = new DynamicParameters();
            foreach (var prop in properties)
            {
                if (prop.GetCustomAttribute<KeyAttribute>() != null)
                {
                    continue;
                }
                var columnAttr = prop.GetCustomAttribute<ColumnAttribute>();
                var columnName = columnAttr != null ? columnAttr.Name : prop.Name;
                setClause += $"{columnName} = @{prop.Name},";
                parameters.Add($"@{prop.Name}", prop.GetValue(entity));
            }
            setClause = setClause.TrimEnd(',');
            var sqlCommand = $"UPDATE {tableName} SET {setClause} WHERE {tableName}_id = @Id";
            parameters.Add("@Id", id);
            var result = dbConnection.ExecuteAsync(sqlCommand, parameters);

            return result;
        }

        public async Task<int> DeleteAsync(Guid id)
        {
            var tableAttr = typeof(T).GetCustomAttribute<TableAttribute>();
            var tableName = tableAttr != null ? tableAttr.Name : typeof(T).Name.ToLower();

            var sqlCommand = $"DELETE FROM {tableName} WHERE {tableName}_id = @Id";

            var result = await dbConnection.ExecuteAsync(sqlCommand, new { Id = id });

            return result;
        }

        public void Dispose()
        {
            dbConnection?.Dispose();
        }
    }
}
