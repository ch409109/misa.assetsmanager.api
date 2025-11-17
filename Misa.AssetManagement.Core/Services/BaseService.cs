using Misa.AssetManagement.Core.Exceptions;
using Misa.AssetManagement.Core.Interfaces.Repositories;
using Misa.AssetManagement.Core.Interfaces.Services;
using Misa.AssetManagement.Core.MISAAttributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
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

        public async Task<T> CreateAsync(T entity)
        {
            await ValidateEntityAsync(entity);
            return await baseRepository.CreateAsync(entity);
        }

        public Task<int> DeleteAsync(string id)
        {
            return baseRepository.DeleteAsync(id);
        }

        public Task<T?> GetByIdAsync(string id)
        {
            return baseRepository.GetByIdAsync(id);
        }

        public async Task<int> UpdateAsync(string id, T entity)
        {
            await ValidateEntityAsync(entity, id);
            return await baseRepository.UpdateAsync(id, entity);
        }

        public async Task<bool> ValidateEntityAsync(T entity, string? excludeId = null)
        {
            var properties = typeof(T).GetProperties();

            foreach (var prop in properties)
            {
                var requiredAttr = prop.GetCustomAttributes(typeof(MISARequired), true);
                if (requiredAttr.Length > 0)
                {
                    var value = prop.GetValue(entity);
                    if (value == null || (value is string str && string.IsNullOrWhiteSpace(str)))
                    {
                        throw new ValidationException($"{prop.Name} không được để trống.");
                    }
                }
            }

            foreach (var prop in properties)
            {
                var duplicateAttr = prop.GetCustomAttribute<MISACheckDuplicate>();
                if (duplicateAttr != null)
                {
                    var value = prop.GetValue(entity);
                    if (value != null)
                    {
                        var columnAttr = prop.GetCustomAttribute<MISAColumnName>();
                        var columnName = columnAttr != null ? columnAttr.ColumnName : prop.Name.ToLower();

                        var isDuplicate = await baseRepository.CheckDuplicateAsync(columnName, value, excludeId);
                        if (isDuplicate)
                        {
                            throw new DuplicateException("Không được phép trùng lặp giá trị.", prop.Name, value.ToString() ?? "");
                        }
                    }
                }
            }
            return true;
        }
    }
}
