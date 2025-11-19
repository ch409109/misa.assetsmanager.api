using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Misa.AssetManagement.Core.Entities;
using Misa.AssetManagement.Core.Interfaces.Services;

namespace Misa.AssetManagement.API.Controllers
{
    /// <summary>
    /// Controller xử lý các API liên quan đến loại tài sản
    /// </summary>
    /// Created by: CongHT - 19/11/2025
    [Route("api/[controller]")]
    [ApiController]
    public class AssetTypesController(IAssetTypeService assetTypeService) : BaseController<AssetType>(assetTypeService)
    {
    }
}
