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
    public class AssetTypeService(IBaseRepository<AssetType> baseRepository) : BaseService<AssetType>(baseRepository), IAssetTypeService
    {
    }
}
