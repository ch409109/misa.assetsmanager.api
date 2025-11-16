using Misa.AssetManagement.Core.Interfaces.Repositories;
using Misa.AssetManagement.Core.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Misa.AssetManagement.Core.Services
{
    public class BaseService<T>(IBaseRepository<T> baseRepository) : IBaseService<T> where T : class
    {
        public Task<IEnumerable<T>> GetAllAsync()
        {
            return baseRepository.GetAllAsync();
        }

        public Task<T> CreateAsync(T entity)
        {
            return baseRepository.CreateAsync(entity);
        }

        public Task<int> DeleteAsync(Guid id)
        {
            return baseRepository.DeleteAsync(id);
        }

        public Task<T?> GetByIdAsync(Guid id)
        {
            return baseRepository.GetByIdAsync(id);
        }

        public Task<int> UpdateAsync(Guid id, T entity)
        {
            return baseRepository.UpdateAsync(id, entity);
        }
    }
}
