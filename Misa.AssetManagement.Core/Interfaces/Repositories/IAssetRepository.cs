using Misa.AssetManagement.Core.Dtos;
using Misa.AssetManagement.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Misa.AssetManagement.Core.Interfaces.Repositories
{
    public interface IAssetRepository : IBaseRepository<Asset>
    {
        Task<PagedResult<AssetListDto>> GetAllAssetsWithDetailsAsync(
            int pageSize,
            int pageNumber,
            string? keyword,
            string? departmentName = null,
            string? assetTypeName = null);
    }
}
