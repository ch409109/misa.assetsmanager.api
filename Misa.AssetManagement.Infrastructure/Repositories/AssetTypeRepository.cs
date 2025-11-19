using Microsoft.Extensions.Configuration;
using Misa.AssetManagement.Core.Entities;
using Misa.AssetManagement.Core.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Misa.AssetManagement.Infrastructure.Repositories
{
    /// <summary>
    /// Repository xử lý nghiệp vụ liên quan đến loại tài sản
    /// </summary>
    /// Created by: CongHT - 16/11/2025
    public class AssetTypeRepository(IConfiguration configuration) : BaseRepository<AssetType>(configuration), IAssetTypeRepository
    {
    }
}
