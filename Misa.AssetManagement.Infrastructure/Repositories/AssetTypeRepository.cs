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
    public class AssetTypeRepository(IConfiguration configuration) : BaseRepository<AssetType>(configuration), IAssetTypeRepository
    {
    }
}
