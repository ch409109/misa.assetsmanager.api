using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Misa.AssetManagement.Core.Interfaces.Repositories
{
    public interface IBaseRepository<T> where T : class
    {
        Task<IEnumerable<T>> GetAllAsync();
        Task<T?> GetByIdAsync(string id);
        Task<T> CreateAsync(T entity);
        Task<int> UpdateAsync(string id, T entity);
        Task<int> DeleteAsync(string id);
        Task<bool> CheckDuplicateAsync(string columnName, object value, string? excludeId = null);
    }
}
