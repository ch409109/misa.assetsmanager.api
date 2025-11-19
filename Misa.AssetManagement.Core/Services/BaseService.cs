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
    /// <summary>
    /// Service cơ sở cung cấp các chức năng CRUD chung
    /// </summary>
    /// <typeparam name="T">Kiểu entity</typeparam>
    /// Created by: CongHT - 16/11/2025
    public class BaseService<T>(IBaseRepository<T> baseRepository) : IBaseService<T> where T : class
    {
        /// <summary>
        /// Lấy tất cả bản ghi với khả năng tìm kiếm theo từ khóa
        /// </summary>
        /// <param name="keyword">Từ khóa tìm kiếm (có thể null)</param>
        /// <returns>Danh sách các bản ghi</returns>
        /// Created by: CongHT - 16/11/2025
        public Task<IEnumerable<T>> GetAllAsync(string? keyword)
        {
            return baseRepository.GetAllAsync(keyword);
        }

        /// <summary>
        /// Tạo mới một bản ghi sau khi validate
        /// </summary>
        /// <param name="entity">Đối tượng cần tạo</param>
        /// <returns>Đối tượng đã được tạo</returns>
        /// Created by: CongHT - 16/11/2025
        public async Task<T> CreateAsync(T entity)
        {
            await ValidateEntityAsync(entity);
            return await baseRepository.CreateAsync(entity);
        }

        /// <summary>
        /// Xóa bản ghi theo ID
        /// </summary>
        /// <param name="id">ID của bản ghi cần xóa</param>
        /// <returns>Số bản ghi bị xóa</returns>
        /// Created by: CongHT - 16/11/2025
        public Task<int> DeleteAsync(string id)
        {
            return baseRepository.DeleteAsync(id);
        }

        /// <summary>
        /// Lấy bản ghi theo ID
        /// </summary>
        /// <param name="id">ID của bản ghi</param>
        /// <returns>Bản ghi tìm được hoặc null nếu không tồn tại</returns>
        /// Created by: CongHT - 16/11/2025
        public Task<T?> GetByIdAsync(string id)
        {
            return baseRepository.GetByIdAsync(id);
        }

        /// <summary>
        /// Cập nhật thông tin bản ghi sau khi validate
        /// </summary>
        /// <param name="id">ID của bản ghi cần cập nhật</param>
        /// <param name="entity">Đối tượng chứa thông tin mới</param>
        /// <returns>Số bản ghi bị ảnh hưởng</returns>
        /// Created by: CongHT - 16/11/2025
        public async Task<int> UpdateAsync(string id, T entity)
        {
            await ValidateEntityAsync(entity, id);
            return await baseRepository.UpdateAsync(id, entity);
        }

        /// <summary>
        /// Validate thông tin entity (kiểm tra required và duplicate)
        /// </summary>
        /// <param name="entity">Đối tượng cần validate</param>
        /// <param name="excludeId">ID cần loại trừ khi kiểm tra trùng lặp (dùng khi update)</param>
        /// <returns>True nếu hợp lệ</returns>
        /// <exception cref="ValidationException">Khi dữ liệu không hợp lệ</exception>
        /// <exception cref="DuplicateException">Khi có dữ liệu trùng lặp</exception>
        /// Created by: CongHT - 16/11/2025
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
