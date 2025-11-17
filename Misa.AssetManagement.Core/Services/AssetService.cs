using Misa.AssetManagement.Core.Dtos;
using Misa.AssetManagement.Core.Entities;
using Misa.AssetManagement.Core.Interfaces.Repositories;
using Misa.AssetManagement.Core.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Misa.AssetManagement.Core.Services
{
    public class AssetService(IAssetRepository assetRepository) : BaseService<Asset>(assetRepository), IAssetService
    {
        private readonly IAssetRepository _assetRepository = assetRepository;

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
    }
}
