using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Misa.AssetManagement.Core.Interfaces.Services
{
    public interface IBaseService<T> where T : class
    {
        Task<IEnumerable<T>> GetAllAsync(string? keyword);
        Task<T?> GetByIdAsync(string id);
        Task<T> CreateAsync(T entity);
        Task<int> UpdateAsync(string id, T entity);
        Task<int> DeleteAsync(string id);
        Task<bool> ValidateEntityAsync(T entity, string? excludeId = null);
    }
}
