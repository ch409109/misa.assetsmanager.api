using Misa.AssetManagement.Core.Dtos;
using Misa.AssetManagement.Core.Entities;
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
    /// Service xử lý nghiệp vụ liên quan đến tài sản
    /// </summary>
    /// Created by: CongHT - 16/11/2025
    public class AssetService(IAssetRepository assetRepository) : BaseService<Asset>(assetRepository), IAssetService
    {
        private readonly IAssetRepository _assetRepository = assetRepository;

        /// <summary>
        /// Tạo mới tài sản từ DTO
        /// </summary>
        /// <param name="assetCreateDto">DTO chứa thông tin tài sản cần tạo</param>
        /// <returns>Tài sản đã được tạo</returns>
        /// Created by: CongHT - 16/11/2025
        public async Task<Asset> CreateAssetAsync(AssetCreateDto assetCreateDto)
        {
            await ValidateAssetCreateDtoAsync(assetCreateDto);

            var asset = MapToAsset(assetCreateDto);

            return await _assetRepository.CreateAsync(asset);
        }

        /// <summary>
        /// Lấy danh sách tài sản với thông tin chi tiết có phân trang và lọc
        /// </summary>
        /// <param name="pageSize">Số bản ghi trên mỗi trang</param>
        /// <param name="pageNumber">Số thứ tự trang</param>
        /// <param name="keyword">Từ khóa tìm kiếm (có thể null)</param>
        /// <param name="departmentName">Tên phòng ban để lọc (có thể null)</param>
        /// <param name="assetTypeName">Tên loại tài sản để lọc (có thể null)</param>
        /// <returns>Kết quả phân trang chứa danh sách tài sản và thông tin tổng hợp</returns>
        /// Created by: CongHT - 16/11/2025
        public Task<PagedResult<AssetListDto>> GetAllAssetsWithDetailsAsync(
            int pageSize,
            int pageNumber,
            string? keyword,
            string? departmentName = null,
            string? assetTypeName = null)
        {
            if (pageSize <= 0)
            {
                pageSize = 12;
            }

            if (pageNumber <= 0)
            {
                pageNumber = 1;
            }

            return _assetRepository.GetAllAssetsWithDetailsAsync(pageSize, pageNumber, keyword, departmentName, assetTypeName);
        }

        /// <summary>
        /// Validate thông tin tài sản từ DTO
        /// </summary>
        /// <param name="dto">DTO chứa thông tin tài sản cần validate</param>
        /// <param name="excludeId">ID cần loại trừ khi kiểm tra trùng lặp (dùng khi update)</param>
        /// <returns>True nếu hợp lệ</returns>
        /// <exception cref="ValidationException">Khi dữ liệu không hợp lệ</exception>
        /// <exception cref="DuplicateException">Khi có dữ liệu trùng lặp</exception>
        /// Created by: CongHT - 16/11/2025
        public async Task<bool> ValidateAssetCreateDtoAsync(AssetCreateDto dto, string? excludeId = null)
        {
            var properties = typeof(AssetCreateDto).GetProperties();

            foreach (var prop in properties)
            {
                var requiredAttr = prop.GetCustomAttributes(typeof(MISARequired), true);
                if (requiredAttr.Length > 0)
                {
                    var value = prop.GetValue(dto);
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
                    var value = prop.GetValue(dto);
                    if (value != null)
                    {
                        var columnAttr = prop.GetCustomAttribute<MISAColumnName>();
                        var columnName = columnAttr != null ? columnAttr.ColumnName : prop.Name.ToLower();

                        var isDuplicate = await _assetRepository.CheckDuplicateAsync(columnName, value, excludeId);
                        if (isDuplicate)
                        {
                            throw new DuplicateException("Không được phép trùng lặp giá trị.", prop.Name, value.ToString() ?? "");
                        }
                    }
                }
            }
            return true;
        }

        /// <summary>
        /// Chuyển đổi từ AssetCreateDto sang Asset entity
        /// </summary>
        /// <param name="dto">DTO chứa thông tin tài sản</param>
        /// <returns>Asset entity</returns>
        /// Created by: CongHT - 16/11/2025
        private static Asset MapToAsset(AssetCreateDto dto)
        {
            var asset = new Asset
            {
                AssetCode = dto.AssetCode,
                AssetName = dto.AssetName,
                AssetTypeId = dto.AssetTypeId,
                DepartmentId = dto.DepartmentId,
                AssetPurchaseDate = dto.AssetPurchaseDate,
                AssetOriginalCost = dto.AssetOriginalCost,
                AssetQuantity = dto.AssetQuantity,
                AssetUsageYear = dto.AssetUsageYear,
                AssetTrackingStartYear = dto.AssetTrackingStartYear,
                AssetAnnualDepreciation = dto.AssetAnnualDepreciation
            };
            return asset;
        }
    }
}
