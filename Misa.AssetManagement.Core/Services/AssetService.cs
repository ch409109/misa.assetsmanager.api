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
    public class AssetService(IAssetRepository assetRepository) : BaseService<Asset>(assetRepository), IAssetService
    {
        private readonly IAssetRepository _assetRepository = assetRepository;

        public async Task<Asset> CreateAssetAsync(AssetCreateDto assetCreateDto)
        {
            await ValidateAssetCreateDtoAsync(assetCreateDto);

            var asset = MapToAsset(assetCreateDto);

            return await _assetRepository.CreateAsync(asset);
        }

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
